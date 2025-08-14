using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioEvents
{
    //SFX 요청할 때
    public static event Action<SFXType, Transform> OnSFXRequest;
    //BGM 요청할 때
    public static event Action<BGMType, string> OnBGMRequest;
    //현재 BGM 종료 요청할 때
    public static event Action OnBGMEnd;


    //SFX 요청할 때
    public static void InvokeSFXRequest(SFXType key, Transform requestTransform)
    {
        OnSFXRequest?.Invoke(key, requestTransform);
    }
    //BGM 요청할 때
    public static void InvokeBGMRequest(BGMType type, string key)
    {
        OnBGMRequest?.Invoke(type, key);
    }
    //현재 BGM 종료 요청할 때
    public static void InvokeBGMEnd()
    {
        OnBGMEnd?.Invoke();
    }

}
