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
        targetScene = LoadingSceneLoader.targetScene;
        StartCoroutine(LoadSceneAsync(targetScene));
    }
    IEnumerator LoadSceneAsync(string sceneName)
    {
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

        // 로딩 완료 후 잠깐 대기 (예: 페이드아웃용)
        yield return new WaitForSeconds(1f);

        // 실제 씬 전환
        asyncLoad.allowSceneActivation = true;
        //MapEvents.InvokeTargetSceneLoaded();
    }


}
