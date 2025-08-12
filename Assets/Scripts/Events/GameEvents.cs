using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    //보스전 시작될 때 이벤트
    public static event Action<string> OnBossFightStart;
    //보스전 끝날 때 이벤트
    public static event Action<string> OnBossFightEnd;


    //보스전 시작될 때 이벤트
    public static void InvokeBossFightStart(string gameEventID)
    {
        OnBossFightStart?.Invoke(gameEventID);
    }
    //보스전 끝날 때 이벤트
    public static void InvokeBossFightEnd(string gameEventID)
    {
        OnBossFightEnd?.Invoke(gameEventID);
    }

}
