using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemEvents
{
    //SaveManager�� ��ųʸ� ���� �̺�Ʈ
    public static event Action<SaveManager> OnSaveDicKeyRequest;
    //�����϶�� �̺�Ʈ
    public static event Action OnSaveRequest;
    //���̺����� �ε尡 �����ٴ� �̺�Ʈ
    public static event Action OnDataLoadFinished;

    //SaveManager�� ��ųʸ� ���� �̺�Ʈ ����
    public static void InvokeSaveDicKeyRequested(SaveManager saveManager)
    {
        OnSaveDicKeyRequest?.Invoke(saveManager);
    }
    //�����϶�� �̺�Ʈ
    public static void InvokeSaveRequested()
    {
        OnSaveRequest?.Invoke();
    }
    //���̺����� �ε尡 �����ٴ� �̺�Ʈ
    public static void InvokeDataLoadFinished()
    {
        OnDataLoadFinished?.Invoke();
    }

}
