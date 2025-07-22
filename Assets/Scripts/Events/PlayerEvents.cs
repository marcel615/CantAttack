using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEvents
{
    //Player가 스폰될 때
    public static event Action<Transform> OnPlayerSpawned_CameraManager;
    public static event Action<int, int> OnPlayerSpawned_HPUIManager;

    //Player가 데미지를 입을 때
    public static event Action<int, int> OnPlayerDamaged_HPUIManager;

    //Player의 Start()에서 이벤트 발행
    public static void InvokePlayerSpawned(Transform transform, int maxHP, int currentHP)
    {
        OnPlayerSpawned_CameraManager?.Invoke(transform);
        OnPlayerSpawned_HPUIManager?.Invoke(maxHP, currentHP);
    }
    //Player의 OnDamaged()에서 이벤트 발행
    public static void InvokePlayerDamaged(int maxHp, int currentHp)
    {
        OnPlayerDamaged_HPUIManager?.Invoke(maxHp, currentHp);
    }
}
