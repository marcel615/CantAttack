using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    private void Start()
    {
        //StartCoroutine(OpenMainMenuAfterFade(SceneTransitionManager.Instance.fadeTime));
        InputEvents.MainMenu.InvokeMainMenuOpen(InputContext.Boot);
        InputEvents.InvokeContextUpdate(InputContext.MainMenu, true);

    }
    IEnumerator OpenMainMenuAfterFade(float fadeTime)
    {
        //InputEvents.MainMenu.InvokeMainMenuOpen(InputContext.Boot);
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeIn);
        yield return new WaitForSeconds(fadeTime);

        InputEvents.MainMenu.InvokeMainMenuOpen(InputContext.Boot);

        //SceneManager.LoadScene("MainMenu");
    }

}
