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
        //���� ���� �Ҵ�
        fadeTime = LoadingSceneLoader.fadeTime;
        targetScene = LoadingSceneLoader.targetScene;
        sceneChangeType = LoadingSceneLoader.sceneChangeType;

        //���̵� �г� �ݱ� (�ε������� �Ѿ���� ���� ���̵�ƿ����� Ȱ��ȭ�Ǿ��� ���̵� �г�)
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
