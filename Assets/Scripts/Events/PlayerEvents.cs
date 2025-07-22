using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEvents
{
    //Player�� ������ ��
    public static event Action<Transform> OnPlayerSpawned_CameraManager;
    public static event Action<int, int> OnPlayerSpawned_HPUIManager;

    //Player�� �������� ���� ��
    public static event Action<int, int> OnPlayerDamaged_HPUIManager;

    //Player�� Start()���� �̺�Ʈ ����
    public static void InvokePlayerSpawned(Transform transform, int maxHP, int currentHP)
    {
        OnPlayerSpawned_CameraManager?.Invoke(transform);
        OnPlayerSpawned_HPUIManager?.Invoke(maxHP, currentHP);
    }
    //Player�� OnDamaged()���� �̺�Ʈ ����
    public static void InvokePlayerDamaged(int maxHp, int currentHp)
    {
        OnPlayerDamaged_HPUIManager?.Invoke(maxHp, currentHp);
    }
}
