using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEvents
{
    //Player�� ������ ��
    public static event Action<Transform> OnPlayerSpawned;
    public static event Action OnPlayerSpawned_NoArgument;

    //Player�� �������� ���� ��
    public static event Action OnPlayerDamaged;

    //Player�� Start()���� �̺�Ʈ ����
    public static void InvokePlayerSpawned(Transform transform)
    {
        OnPlayerSpawned?.Invoke(transform);
        OnPlayerSpawned_NoArgument?.Invoke();
    }
    //Player�� OnDamaged()���� �̺�Ʈ ����
    public static void InvokePlayerDamaged()
    {
        OnPlayerDamaged?.Invoke();
    }
}
