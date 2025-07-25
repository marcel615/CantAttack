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
    //������ �����ٴ� �̺�Ʈ
    public static event Action OnSaveEnd;
    //���̺����� �ε尡 �����ٴ� �̺�Ʈ
    public static event Action OnDataLoadFinished;

    //SaveManager�� ��ųʸ� ���� �̺�Ʈ ����
    public static void InvokeSaveDicKeyRequested(SaveManager saveManager)
    {
        OnSaveDicKeyRequest?.Invoke(saveManager);
    }
    //�����϶�� �̺�Ʈ
    public static void InvokeSaveRequest()
    {
        OnSaveRequest?.Invoke();
    }
    //������ �����ٴ� �̺�Ʈ
    public static void InvokeSaveEnd()
    {
        OnSaveEnd?.Invoke();
    }
    //���̺����� �ε尡 �����ٴ� �̺�Ʈ
    public static void InvokeDataLoadFinished()
    {
        OnDataLoadFinished?.Invoke();
    }

}
