using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PlayerEvents
{
    //Player�� ������ ��
    public static event Action<Transform> OnPlayerSpawned_CameraManager;
    public static event Action<int, int> OnPlayerSpawned_HPUIManager;

    //PlayerHitBox���� Hit�Ǿ��� ��
    public static event Action<Vector2, int> OnPlayerDamaged_PlayerDamageHandler;

    //PlayerDamageHandler���� ������ ó���� ��
    public static event Action<int, int> OnPlayerDamaged_HPUIManager;

    //Player���� �ǰ� ���� �����ð� ����Ǿ��� ��
    public static event Action OnPlayerKnockedBackInvincibleOver;



    //Player�� Start()���� �̺�Ʈ ����
    public static void InvokePlayerSpawned(Transform transform, int maxHP, int currentHP)
    {
        OnPlayerSpawned_CameraManager?.Invoke(transform);
        OnPlayerSpawned_HPUIManager?.Invoke(maxHP, currentHP);
    }
    //PlayerHitBox���� �̺�Ʈ ����
    public static void InvokePlayerHitBoxHitted(Vector2 hitTargetPos, int damage)
    {
        OnPlayerDamaged_PlayerDamageHandler?.Invoke(hitTargetPos, damage);
    }
    //PlayerDamageHandler���� �̺�Ʈ ����
    public static void InvokePlayerDamaged(int maxHp, int currentHp)
    {
        OnPlayerDamaged_HPUIManager?.Invoke(maxHp, currentHp);
    }
    //Player���� �ǰ� ���� �����ð� ����Ǿ��� ��
    public static void InvokePlayerKnockedBackInvincibleOver()
    {
        OnPlayerKnockedBackInvincibleOver?.Invoke();
    }
}
