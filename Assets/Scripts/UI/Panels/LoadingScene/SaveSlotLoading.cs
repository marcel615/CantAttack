using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotLoading : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI loadingText;

    //SaveSlotLoading 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;



    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (progressBar == null) progressBar = transform.Find("ProgressBar")?.GetComponent<Slider>();
        if (loadingText == null) loadingText = transform.Find("LoadingText")?.GetComponent<TextMeshProUGUI>();
    }

    //어디선가 SaveSlotLoading 패널을 열었을 때
    public void SaveSlotLoadingOpen(float fadeTime, string targetScene, int slotNum)
    {
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, gameObject);

        //TargetScene 로딩 진행
        StartCoroutine(LoadSceneAsync(fadeTime, targetScene, slotNum));

    }
    public void SaveSlotLoadingClose()
    {
        if (currentPanel != null)
        {
            UIPanelController.Close(ref currentPanel, gameObject);
        }
    }

    IEnumerator LoadSceneAsync(float fadeTime, string targetScene, int slotNum)
    {
        // 로딩 전 잠깐 대기 (페이드인)
        LoadingSceneEvents.InvokeLoadingSceneFadeOpen(fadeTime, FadeDirection.FadeIn);
        yield return new WaitForSeconds(fadeTime);

        // 세이브파일 로드 시작
        bool loadFinished = false;

        void OnLoadFinished()
        {
            targetScene = MapManager.Instance.saveScene;    //타겟 씬 이름 알아냄
            loadFinished = true;
            SystemEvents.OnDataLoadFinished -= OnLoadFinished;
        }
        SystemEvents.OnDataLoadFinished += OnLoadFinished;

        SystemEvents.InvokeDataLoadStart(slotNum);

        // 세이브파일 로드 완료까지 대기
        yield return new WaitUntil(() => loadFinished);

        //비동기 씬 로딩 시작
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);
        asyncLoad.allowSceneActivation = false;

        // 90%까지 로딩 완료될 때까지 대기
        while (asyncLoad.progress < 0.9f)
        {
            // 로딩바 등 업데이트
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar.value = progress;
            loadingText.text = $"Loading... {(int)(progress * 100)}%";

            yield return null;
        }

        // 강제 대기 (테스트용)
        float fakeWait = 2f;
        float timer = 0f;
        while (timer < fakeWait)
        {
            timer += Time.deltaTime;
            progressBar.value = Mathf.Lerp(0.9f, 1f, timer / fakeWait);
            loadingText.text = $"Loading... {(int)(progressBar.value * 100)}%";
            yield return null;
        }

        progressBar.value = 1f;
        loadingText.text = $"Loading... 100%";

        // 로딩 완료 후 잠깐 대기 (페이드아웃)
        LoadingSceneEvents.InvokeLoadingSceneFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        // 실제 씬 전환
        asyncLoad.allowSceneActivation = true;
    }
}
