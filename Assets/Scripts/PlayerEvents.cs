using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEvents
{
    //Player�� ������ ��
    public static event Action<Transform> OnPlayerSpawned;

    //Player�� Start()���� �̺�Ʈ ����
    public static void InvokePlayerSpawned(Transform transform)
    {
        OnPlayerSpawned?.Invoke(transform);
    }
}
