using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingSceneLoader
{
    public static float fadeTime;
    public static string targetScene;
    public static SceneChangeType sceneChangeType;
    public static void Init(float fadeT, string target, SceneChangeType changeType)
    {
        fadeTime = fadeT;
        targetScene = target;
        sceneChangeType = changeType;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
    }
}
