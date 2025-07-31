using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI loadingText;

    string targetScene;

    private void Start()
    {
        FadeEvents.InvokeFadeClose();
        targetScene = LoadingSceneLoader.targetScene;
        StartCoroutine(LoadSceneAsync(targetScene));
    }
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // �ε� �� ��� ��� (���̵���)
        float fadeTime = 1f;
        FadeEvents.InvokeLoadingSceneFadeOpen(fadeTime, FadeDirection.FadeIn);
        yield return new WaitForSeconds(fadeTime);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
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
        fadeTime = 1f;
        FadeEvents.InvokeLoadingSceneFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        // ���� �� ��ȯ
        asyncLoad.allowSceneActivation = true;
        //MapEvents.InvokeTargetSceneLoaded();
    }


}
