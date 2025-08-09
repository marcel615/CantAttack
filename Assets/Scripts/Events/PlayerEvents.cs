using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PlayerEvents
{
    //Player�� ������ ��
    public static event Action<Player> OnPlayerInstance;
    public static event Action<int, int> OnPlayerSpawned_HPUIManager;

    //PlayerHitBox���� Hit�Ǿ��� ��
    public static event Action<Vector2, int> OnPlayerHitBoxHitted_PlayerDamageHandler;

    //PlayerDamageHandler���� ������ ó���� ��
    public static event Action<int, int> OnPlayerDamaged_HPUIManager;

    //Player���� �ǰ� ���� �����ð� ����Ǿ��� ��
    public static event Action OnPlayerKnockedBackInvincibleOver;

    //PlayerParry�� �������� ��
    public static event Action OnPlayerParrySuccess;

    //Player�� ������� ��
    public static event Action OnPlayerDead;



    //Player�� Start()���� �̺�Ʈ ����
    public static void InvokePlayerInstance(Player player)
    {
        OnPlayerInstance?.Invoke(player);
    }
    //Player�� Start()���� �̺�Ʈ ����
    public static void InvokePlayerSpawned_HPUIManager(int maxHP, int currentHP)
    {
        OnPlayerSpawned_HPUIManager?.Invoke(maxHP, currentHP);
    }
    //PlayerHitBox���� �̺�Ʈ ����
    public static void InvokePlayerHitBoxHitted(Vector2 hitTargetPos, int damage)
    {
        OnPlayerHitBoxHitted_PlayerDamageHandler?.Invoke(hitTargetPos, damage);
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
    //PlayerParry�� �������� ��
    public static void InvokePlayerParrySuccess()
    {
        OnPlayerParrySuccess?.Invoke();
    }
    //Player�� ������� ��
    public static void InvokePlayerDead()
    {
        OnPlayerDead?.Invoke();
    }
}
