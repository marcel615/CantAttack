using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    //문 Active 요청 이벤트
    public static event Action<string> OnRoomGateActive;
    //문 DeActive 요청 이벤트
    public static event Action<string> OnRoomGateDeActive;

    //보스전 시작될 때 이벤트
    public static event Action<string, Transform> OnBossFightStart;
    //보스전 끝날 때 이벤트
    public static event Action OnBossFightEnd;

    //도전 구역(Trial Area) 시작될 때 이벤트
    public static event Action<string> OnTrialAreaStart;
    //도전 구역(Trial Area) 끝날 때 이벤트
    public static event Action OnTrialAreaEnd;


    //문 Active 요청 이벤트
    public static void InvokeRoomGateActive(string gameEventID)
    {
        OnRoomGateActive?.Invoke(gameEventID);
    }
    //문 DeActive 요청 이벤트
    public static void InvokeRoomGateDeActive(string gameEventID)
    {
        OnRoomGateDeActive?.Invoke(gameEventID);
    }

    //보스전 시작될 때 이벤트
    public static void InvokeBossFightStart(string gameEventID, Transform bossTransform)
    {
        OnBossFightStart?.Invoke(gameEventID, bossTransform);
    }
    //보스전 끝날 때 이벤트
    public static void InvokeBossFightEnd()
    {
        OnBossFightEnd?.Invoke();
    }

    //도전 구역(Trial Area) 시작될 때 이벤트
    public static void InvokeTrialAreaStart(string gameEventID)
    {
        OnTrialAreaStart?.Invoke(gameEventID);
    }
    //도전 구역(Trial Area) 끝날 때 이벤트
    public static void InvokeTrialAreaEnd()
    {
        OnTrialAreaEnd?.Invoke();
    }

}
