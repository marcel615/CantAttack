using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapEvents
{
    //MapManager가 스폰될 때
    public static event Action<MapManager> OnMapManagerInstance;
    //LocalMapManager가 스폰될 때
    public static event Action<LocalMapManager> OnLocalMapManagerInit;

    //Map을 바꾸라는 요청이 들어올 때
    public static event Action OnRequestMapMove;


    //LocalMapManager가 스폰될 때
    public static void InvokeMapManagerInstance(MapManager mapManager)
    {
        OnMapManagerInstance?.Invoke(mapManager);
    }
    //LocalMapManager가 스폰될 때
    public static void InvokeLocalMapManagerInit(LocalMapManager localMapManager)
    {
        OnLocalMapManagerInit?.Invoke(localMapManager);
    }

    //Map을 바꾸라는 요청이 들어올 때
    public static void InvokeRequestMapMove()
    {
        OnRequestMapMove?.Invoke();
    }

}
