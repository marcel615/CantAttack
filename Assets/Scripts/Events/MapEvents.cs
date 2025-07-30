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

    //Map�� �ٲٶ�� ��û�� ���� ��
    public static event Action OnRequestMapMove;


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

    //Map�� �ٲٶ�� ��û�� ���� ��
    public static void InvokeRequestMapMove()
    {
        OnRequestMapMove?.Invoke();
    }

}
