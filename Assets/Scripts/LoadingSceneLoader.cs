using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingSceneLoader
{
    public static SceneChangeType sceneChangeType;
    public static float fadeTime;
    public static string targetScene;
    public static int slotNum;
    public static void Init(SceneChangeType changeType, float fadeT, string target, int num)
    {
        sceneChangeType = changeType;
        fadeTime = fadeT;
        targetScene = target;
        slotNum = num;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
    }
}
