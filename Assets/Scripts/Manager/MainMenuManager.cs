using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    private void Start()
    {
        /*
        //���� ���� �Ҵ�
        sceneChangeType = LoadingSceneLoader.sceneChangeType;
        fadeTime = LoadingSceneLoader.fadeTime;
        targetScene = LoadingSceneLoader.targetScene;
        slotNum = LoadingSceneLoader.slotNum;
        */

        //���̵��� ����(���θ޴� ������ �Ѿ���� ���� ���̵�ƿ��̾��� ��)
        //FadeEvents.InvokeFadeOpen(SceneTransitionManager.Instance.fadeTime, FadeDirection.FadeIn);
        InputEvents.MainMenu.InvokeMainMenuOpen(InputContext.Boot);
        //FadeEvents.InvokeFadeClose();

    }

}
