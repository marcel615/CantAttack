using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemEvents
{
    //SaveManager의 딕셔너리 설정 이벤트
    public static event Action<SaveManager> OnSaveDicKeyRequest;

    //저장하라는 이벤트
    public static event Action OnSaveRequest;
    //저장이 끝났다는 이벤트
    public static event Action OnSaveEnd;

    //세이브파일 로드 시작하라는 이벤트
    public static event Action<int> OnDataLoadStart;
    //세이브파일 로드 끝났다는 이벤트
    public static event Action OnDataLoadFinished;

    //SaveManager의 딕셔너리 설정 이벤트 발행
    public static void InvokeSaveDicKeyRequested(SaveManager saveManager)
    {
        OnSaveDicKeyRequest?.Invoke(saveManager);
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

}
