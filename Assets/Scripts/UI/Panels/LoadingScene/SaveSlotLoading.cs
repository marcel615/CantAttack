using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotLoading : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI loadingText;

    //SaveSlotLoading ���� ���� ����
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;



    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (progressBar == null) progressBar = transform.Find("ProgressBar")?.GetComponent<Slider>();
        if (loadingText == null) loadingText = transform.Find("LoadingText")?.GetComponent<TextMeshProUGUI>();
    }

    //��𼱰� SaveSlotLoading �г��� ������ ��
    public void SaveSlotLoadingOpen(float fadeTime, string targetScene, int slotNum)
    {
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, gameObject);

        //TargetScene �ε� ����
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
        // �ε� �� ��� ��� (���̵���)
        LoadingSceneEvents.InvokeLoadingSceneFadeOpen(fadeTime, FadeDirection.FadeIn);
        yield return new WaitForSeconds(fadeTime);

        // ���̺����� �ε� ����
        bool loadFinished = false;

        void OnLoadFinished()
        {
            targetScene = MapManager.Instance.saveScene;    //Ÿ�� �� �̸� �˾Ƴ�
            loadFinished = true;
            SystemEvents.OnDataLoadFinished -= OnLoadFinished;
        }
        SystemEvents.OnDataLoadFinished += OnLoadFinished;

        SystemEvents.InvokeDataLoadStart(slotNum);

        // ���̺����� �ε� �Ϸ���� ���
        yield return new WaitUntil(() => loadFinished);

        //�񵿱� �� �ε� ����
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);
        asyncLoad.allowSceneActivation = false;

        // 90%���� �ε� �Ϸ�� ������ ���
        while (asyncLoad.progress < 0.9f)
        {
            // �ε��� �� ������Ʈ
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar.value = progress;
            loadingText.text = $"Loading... {(int)(progress * 100)}%";

            yield return null;
        }

        // ���� ��� (�׽�Ʈ��)
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

        // �ε� �Ϸ� �� ��� ��� (���̵�ƿ�)
        LoadingSceneEvents.InvokeLoadingSceneFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        // ���� �� ��ȯ
        asyncLoad.allowSceneActivation = true;
    }
}
