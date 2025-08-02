using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootInitializer : MonoBehaviour
{
    private void Start()
    {
        // 매니저 초기화
        //GameManager.Instance.Init();
        UIManager.Instance.Init();
        //SaveManager.Instance.Init();
        //InputManager.Instance.Init();
        //MessageManager.Instance.Init();
        Player.Instance.Init();
        MapManager.Instance.Init();
        //SceneTransitionManager.Instance.Init();

        // 메인메뉴 로드
        //StartCoroutine(OpenMainMenuAfterFade(SceneTransitionManager.Instance.fadeTime));
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator OpenMainMenuAfterFade(float fadeTime)
    {
        //InputEvents.MainMenu.InvokeMainMenuOpen(InputContext.Boot);
        //FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeIn);
        yield return new WaitForSeconds(3f);
        Debug.Log("Test");
        SceneManager.LoadScene("MainMenu");
    }
}
