using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    private void Start()
    {
        /*
        //제어 변수 할당
        sceneChangeType = LoadingSceneLoader.sceneChangeType;
        fadeTime = LoadingSceneLoader.fadeTime;
        targetScene = LoadingSceneLoader.targetScene;
        slotNum = LoadingSceneLoader.slotNum;
        */

        //페이드인 실행(메인메뉴 씬으로 넘어오기 전에 페이드아웃이었을 것)
        //FadeEvents.InvokeFadeOpen(SceneTransitionManager.Instance.fadeTime, FadeDirection.FadeIn);
        InputEvents.MainMenu.InvokeMainMenuOpen(InputContext.Boot);
        //FadeEvents.InvokeFadeClose();

    }

}
