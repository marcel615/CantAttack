using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemEvents
{
    //SaveManager�� ��ųʸ� ���� �̺�Ʈ
    public static event Action<SaveManager> OnSaveDicKeyRequest;

    //SavePoint���� �����ϱ� ������ ������ �̺�Ʈ
    public static event Action<Transform> OnSavePointNotice;

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

    //MapManager���� �ش� Scene������ map�� minpos�� maxpos�� ���Ϸ��ߴٴ� �̺�Ʈ
    public static event Action<Vector3, Vector3> OnGetMapPos;

    //GameManager���� TimeScale �ٲ�޶�� ��û�ϴ� �̺�Ʈ
    public static event Action<float> OnChangeTimeScale;



    //SaveManager�� ��ųʸ� ���� �̺�Ʈ ����
    public static void InvokeSaveDicKeyRequested(SaveManager saveManager)
    {
        OnSaveDicKeyRequest?.Invoke(saveManager);
    }

    //SavePoint���� �����ϱ� ������ ������ �̺�Ʈ
    public static void InvokeSavePointNotice(Transform transform)
    {
        OnSavePointNotice?.Invoke(transform);
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
    
    //MapManager���� �ش� Scene������ map�� minpos�� maxpos�� ���Ϸ��ߴٴ� �̺�Ʈ
    public static void InvokeGetMapPos(Vector3 minPos, Vector3 maxPos)
    {
        OnGetMapPos?.Invoke(minPos, maxPos);
    }

    //GameManager���� TimeScale �ٲ�޶�� ��û�ϴ� �̺�Ʈ
    public static void InvokeChangeTimeScale(float timeScale)
    {
        OnChangeTimeScale?.Invoke(timeScale);
    }
}
