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

    //포탈을 통한 Map을 바꾸라는 요청이 들어올 때
    public static event Action OnRequestMapMove_Portal;
    //Map 바꾸겠다고 이벤트 발행
    public static event Action OnStartChangeScene;
    //새로 진입한 씬에서 PlayerPosition값 새로 획득했을 때
    public static event Action<Vector2> OnGetPlayerPos;


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
    public static void InvokeRequestMapMove_Portal()
    {
        OnRequestMapMove_Portal?.Invoke();
    }
    //Map 바꾸겠다고 이벤트 발행
    public static void InvokeStartChangeScene()
    {
        OnStartChangeScene?.Invoke();
    }
    //새로 진입한 씬에서 PlayerPosition값 새로 획득했을 때
    public static void InvokeGetPlayerPos(Vector2 playerPos)
    {
        OnGetPlayerPos?.Invoke(playerPos);
    }

}
