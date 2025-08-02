using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneTransitionEvents
{
    //���θ޴� Continue ��ư���� ���� ������ �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
    public static event Action OnContinueToGameScene;
    //���θ޴� ���̺� ���Կ��� ���� ������ �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
    public static event Action<int> OnSaveSlotToGameScene;
    //��Ż���� ��Ż�� �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
    public static event Action<string> OnPortalToPortal;

    //���θ޴� Continue ��ư���� ���� ������ �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
    public static void InvokeContinueToGameScene()
    {
        OnContinueToGameScene?.Invoke();
    }
    //���θ޴� ���̺� ���Կ��� ���� ������ �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
    public static void InvokeSaveSlotToGameScene(int slotNum)
    {
        OnSaveSlotToGameScene?.Invoke(slotNum);
    }
    //��Ż���� ��Ż�� �� ��ȯ ��û�ϴ� ��� �̺�Ʈ
    public static void InvokePortalToPortal(string targetScene)
    {
        OnPortalToPortal?.Invoke(targetScene);
    }

}
