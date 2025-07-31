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
        // 로딩 전 잠깐 대기 (페이드인)
        float fadeTime = 1f;
        FadeEvents.InvokeLoadingSceneFadeOpen(fadeTime, FadeDirection.FadeIn);
        yield return new WaitForSeconds(fadeTime);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
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
        fadeTime = 1f;
        FadeEvents.InvokeLoadingSceneFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        // 실제 씬 전환
        asyncLoad.allowSceneActivation = true;
        //MapEvents.InvokeTargetSceneLoaded();
    }


}
