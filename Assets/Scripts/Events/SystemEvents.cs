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
    //세이브파일 로드가 끝났다는 이벤트
    public static event Action OnDataLoadFinished;

    //SaveManager의 딕셔너리 설정 이벤트 발행
    public static void InvokeSaveDicKeyRequested(SaveManager saveManager)
    {
        OnSaveDicKeyRequest?.Invoke(saveManager);
    }
    //저장하라는 이벤트
    public static void InvokeSaveRequested()
    {
        OnSaveRequest?.Invoke();
    }
    //세이브파일 로드가 끝났다는 이벤트
    public static void InvokeDataLoadFinished()
    {
        OnDataLoadFinished?.Invoke();
    }

}
