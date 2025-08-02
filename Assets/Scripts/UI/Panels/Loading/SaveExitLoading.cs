using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveExitLoading : MonoBehaviour
{
    //�θ� �г�
    [SerializeField] private GameObject loadingPanel;

    //�ڽ� ������Ʈ
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI loadingText;

    //PortalLoading ���� ���� ����
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;



    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (progressBar == null) progressBar = transform.Find("ProgressBar")?.GetComponent<Slider>();
        if (loadingText == null) loadingText = transform.Find("LoadingText")?.GetComponent<TextMeshProUGUI>();
    }

    //��𼱰� SaveExitLoading �г��� ������ ��
    public void SaveExitLoadingOpen(float fadeTime, string targetScene)
    {
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, loadingPanel);

        //TargetScene �ε� ����
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
        // �ε� �� ��� ��� (���̵���)
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeIn);
        yield return new WaitForSeconds(fadeTime);

        // ���̺� ����
        bool SaveFinished = false;

        void OnSaveFinished()
        {
            SaveFinished = true;
            SystemEvents.OnSaveEnd -= OnSaveFinished;
        }
        SystemEvents.OnSaveEnd += OnSaveFinished;

        //���̺� ��û �̺�Ʈ ����
        SystemEvents.InvokeSaveRequest();

        // ���̺����� �ε� �Ϸ���� ���
        yield return new WaitUntil(() => SaveFinished);

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

        // �ε� �Ϸ� �� ��� ��� (���̵�ƿ�)
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //�� �г� �ݱ�
        ResetPanel();
        SaveExitLoadingClose();

        // ���� �� ��ȯ
        asyncLoad.allowSceneActivation = true;
    }
}
