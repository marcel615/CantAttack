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

    //���̺����� �ε� �����϶�� �̺�Ʈ
    public static event Action<int> OnDataLoadStart;
    //���̺����� �ε� �����ٴ� �̺�Ʈ
    public static event Action OnDataLoadFinished;

    //���̺������� ���ٸ� �����ϰ�, �ִٸ� ���� �ֱ� ���̺����� ������ �ϴ� �̺�Ʈ
    public static event Action ONNewGameORLatestSave;


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

    //���̺����� �ε� �����϶�� �̺�Ʈ
    public static void InvokeDataLoadStart(int num)
    {
        OnDataLoadStart?.Invoke(num);
    }
    //���̺����� �ε� �����ٴ� �̺�Ʈ
    public static void InvokeDataLoadFinished()
    {
        OnDataLoadFinished?.Invoke();
    }

    //���̺������� ���ٸ� �����ϰ�, �ִٸ� ���� �ֱ� ���̺����� ������ �ϴ� �̺�Ʈ
    public static void InvokeNewGameORLatestSave()
    {
        ONNewGameORLatestSave?.Invoke();
    }

}
