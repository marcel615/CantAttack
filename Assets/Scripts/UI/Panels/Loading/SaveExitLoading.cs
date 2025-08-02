using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveExitLoading : MonoBehaviour
{
    //부모 패널
    [SerializeField] private GameObject loadingPanel;

    //자식 오브젝트
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI loadingText;

    //PortalLoading 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;



    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (progressBar == null) progressBar = transform.Find("ProgressBar")?.GetComponent<Slider>();
        if (loadingText == null) loadingText = transform.Find("LoadingText")?.GetComponent<TextMeshProUGUI>();
    }

    //어디선가 SaveExitLoading 패널을 열었을 때
    public void SaveExitLoadingOpen(float fadeTime, string targetScene)
    {
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, loadingPanel);

        //TargetScene 로딩 진행
        StartCoroutine(LoadSceneAsync(fadeTime, targetScene));

    }
    public void SaveExitLoadingClose()
    {
        if (currentPanel != null)
        {
            UIPanelController.Close(ref currentPanel, loadingPanel);
        }
    }
    void ResetPanel()
    {
        progressBar.value = 0f;
        loadingText.text = $"Loading... 0%";
    }

    IEnumerator LoadSceneAsync(float fadeTime, string targetScene)
    {
        // 로딩 전 잠깐 대기 (페이드인)
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeIn);
        yield return new WaitForSeconds(fadeTime);

        // 세이브 시작
        bool SaveFinished = false;

        void OnSaveFinished()
        {
            SaveFinished = true;
            SystemEvents.OnSaveEnd -= OnSaveFinished;
        }
        SystemEvents.OnSaveEnd += OnSaveFinished;

        //세이브 요청 이벤트 발행
        SystemEvents.InvokeSaveRequest();

        // 세이브파일 로드 완료까지 대기
        yield return new WaitUntil(() => SaveFinished);

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
        float fakeWait = 3f;
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
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //이 패널 닫기
        ResetPanel();
        SaveExitLoadingClose();

        // 실제 씬 전환
        asyncLoad.allowSceneActivation = true;
    }
}
