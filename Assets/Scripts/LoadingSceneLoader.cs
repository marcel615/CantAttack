using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingSceneLoader
{
    public static string targetScene;

    public static void LoadScene(string sceneName)
    {
        targetScene = sceneName;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
    }
}
