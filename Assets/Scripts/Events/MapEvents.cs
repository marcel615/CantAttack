using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapEvents
{
    //MapManager�� ������ ��
    public static event Action<MapManager> OnMapManagerInstance;
    //LocalMapManager�� ������ ��
    public static event Action<LocalMapManager> OnLocalMapManagerInit;

    //���� ������ ������ PlayerPosition�� ���� ȹ������ ��
    public static event Action<Vector2> OnGetPlayerPos;
    //���̺꽽�Կ��� ���Ӿ����� �ε尡 �Ϸ�Ǿ��� ��
    public static event Action OnSavedSceneLoaded;
    //�÷��̾ �������ؼ� ���Ӿ����� �ε尡 �Ϸ�Ǿ��� ��
    public static event Action OnRespawnSceneLoaded;


    //LocalMapManager�� ������ ��
    public static void InvokeMapManagerInstance(MapManager mapManager)
    {
        OnMapManagerInstance?.Invoke(mapManager);
    }
    //LocalMapManager�� ������ ��
    public static void InvokeLocalMapManagerInit(LocalMapManager localMapManager)
    {
        OnLocalMapManagerInit?.Invoke(localMapManager);
    }

    //���� ������ ������ PlayerPosition�� ���� ȹ������ ��
    public static void InvokeGetPlayerPos(Vector2 playerPos)
    {
        OnGetPlayerPos?.Invoke(playerPos);
    }
    //���̺꽽�Կ��� ���Ӿ����� �ε尡 �Ϸ�Ǿ��� ��
    public static void InvokeSavedSceneLoaded()
    {
        OnSavedSceneLoaded?.Invoke();
    }
    //�÷��̾ �������ؼ� ���Ӿ����� �ε尡 �Ϸ�Ǿ��� ��
    public static void InvokeRespawnSceneLoaded()
    {
        OnRespawnSceneLoaded?.Invoke();
    }

}
