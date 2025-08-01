using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    float fadeTime;
    string targetScene;
    SceneChangeType sceneChangeType;

    private void Start()
    {
        //제어 변수 할당
        fadeTime = LoadingSceneLoader.fadeTime;
        targetScene = LoadingSceneLoader.targetScene;
        sceneChangeType = LoadingSceneLoader.sceneChangeType;

        //페이드 패널 닫기 (로딩씬으로 넘어오기 전에 페이드아웃으로 활성화되었던 페이드 패널)
        FadeEvents.InvokeFadeClose();

        switch (sceneChangeType)
        {
            case SceneChangeType.SaveSlot:
                LoadingSceneEvents.InvokeSaveSlotLoadingOpen(fadeTime, targetScene);

                break;
            case SceneChangeType.Portal:
                LoadingSceneEvents.InvokePortalLoadingOpen(fadeTime, targetScene);

                break;
        }
    }


}
