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

    //새로 진입한 씬에서 PlayerPosition값 새로 획득했을 때
    public static event Action<Vector2> OnGetPlayerPos;
    //세이브슬롯에서 게임씬으로 로드가 완료되었을 때
    public static event Action OnSavedSceneLoaded;
    //플레이어가 리스폰해서 게임씬으로 로드가 완료되었을 때
    public static event Action OnRespawnSceneLoaded;


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

    //새로 진입한 씬에서 PlayerPosition값 새로 획득했을 때
    public static void InvokeGetPlayerPos(Vector2 playerPos)
    {
        OnGetPlayerPos?.Invoke(playerPos);
    }
    //세이브슬롯에서 게임씬으로 로드가 완료되었을 때
    public static void InvokeSavedSceneLoaded()
    {
        OnSavedSceneLoaded?.Invoke();
    }
    //플레이어가 리스폰해서 게임씬으로 로드가 완료되었을 때
    public static void InvokeRespawnSceneLoaded()
    {
        OnRespawnSceneLoaded?.Invoke();
    }

}
