using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemEvents
{
    //SaveManager의 딕셔너리 설정 이벤트
    public static event Action<SaveManager> OnSaveDicKeyRequest;

    //SavePoint에서 저장하기 직전에 보내는 이벤트
    public static event Action<Transform> OnSavePointNotice;

    //저장하라는 이벤트
    public static event Action OnSaveRequest;
    //저장이 끝났다는 이벤트
    public static event Action OnSaveEnd;

    //세이브파일 로드 시작하라는 이벤트
    public static event Action<int> OnDataLoadStart;
    //세이브파일 로드 끝났다는 이벤트
    public static event Action OnDataLoadFinished;

    //세이브파일이 없다면 생성하고, 있다면 가장 최근 세이브파일 열도록 하는 이벤트
    public static event Action ONNewGameORLatestSave;

    //MapManager에서 해당 Scene에서의 map의 minpos와 maxpos를 계산완료했다는 이벤트
    public static event Action<Vector3, Vector3> OnGetMapPos;

    //GameManager한테 TimeScale 바꿔달라고 요청하는 이벤트
    public static event Action<float> OnChangeTimeScale;



    //SaveManager의 딕셔너리 설정 이벤트 발행
    public static void InvokeSaveDicKeyRequested(SaveManager saveManager)
    {
        OnSaveDicKeyRequest?.Invoke(saveManager);
    }

    //SavePoint에서 저장하기 직전에 보내는 이벤트
    public static void InvokeSavePointNotice(Transform transform)
    {
        OnSavePointNotice?.Invoke(transform);
    }

    //저장하라는 이벤트
    public static void InvokeSaveRequest()
    {
        OnSaveRequest?.Invoke();
    }
    //저장이 끝났다는 이벤트
    public static void InvokeSaveEnd()
    {
        OnSaveEnd?.Invoke();
    }

    //세이브파일 로드 시작하라는 이벤트
    public static void InvokeDataLoadStart(int num)
    {
        OnDataLoadStart?.Invoke(num);
    }
    //세이브파일 로드 끝났다는 이벤트
    public static void InvokeDataLoadFinished()
    {
        OnDataLoadFinished?.Invoke();
    }

    //세이브파일이 없다면 생성하고, 있다면 가장 최근 세이브파일 열도록 하는 이벤트
    public static void InvokeNewGameORLatestSave()
    {
        ONNewGameORLatestSave?.Invoke();
    }
    
    //MapManager에서 해당 Scene에서의 map의 minpos와 maxpos를 계산완료했다는 이벤트
    public static void InvokeGetMapPos(Vector3 minPos, Vector3 maxPos)
    {
        OnGetMapPos?.Invoke(minPos, maxPos);
    }

    //GameManager한테 TimeScale 바꿔달라고 요청하는 이벤트
    public static void InvokeChangeTimeScale(float timeScale)
    {
        OnChangeTimeScale?.Invoke(timeScale);
    }
}
