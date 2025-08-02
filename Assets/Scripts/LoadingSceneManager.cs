using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    SceneChangeType sceneChangeType;
    float fadeTime;
    string targetScene;
    int slotNum;

    private void Start()
    {
        //���� ���� �Ҵ�
        sceneChangeType = LoadingSceneLoader.sceneChangeType;
        fadeTime = LoadingSceneLoader.fadeTime;
        targetScene = LoadingSceneLoader.targetScene;
        slotNum = LoadingSceneLoader.slotNum;

        //���̵� �г� �ݱ� (�ε������� �Ѿ���� ���� ���̵�ƿ����� Ȱ��ȭ�Ǿ��� ���̵� �г�)
        FadeEvents.InvokeFadeClose();

        switch (sceneChangeType)
        {
            case SceneChangeType.MainMenuContinue:
                LoadingSceneEvents.InvokeSaveSlotLoadingOpen(sceneChangeType,fadeTime, targetScene, slotNum);

                break;

            case SceneChangeType.SaveSlot:
                LoadingSceneEvents.InvokeSaveSlotLoadingOpen(sceneChangeType, fadeTime, targetScene, slotNum);

                break;

            case SceneChangeType.Portal:
                LoadingSceneEvents.InvokePortalLoadingOpen(fadeTime, targetScene);

                break;
        }
    }


}
