using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneTransitionEvents
{
    //���θ޴� ���̺� ���Կ��� ���� ������ �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
    public static event Action<string> OnSaveSlotToGameScene;
    //��Ż���� ��Ż�� �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
    public static event Action<string> OnPortalToPortal;

    //���θ޴� ���̺� ���Կ��� ���� ������ �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
    public static void InvokeSaveSlotToGameScene(string targetScene)
    {
        OnSaveSlotToGameScene?.Invoke(targetScene);
    }
    //��Ż���� ��Ż�� �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
    public static void InvokePortalToPortal(string targetScene)
    {
        OnPortalToPortal?.Invoke(targetScene);
    }

}
