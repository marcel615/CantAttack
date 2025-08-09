using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PlayerEvents
{
    //Player가 스폰될 때
    public static event Action<Player> OnPlayerInstance;
    public static event Action<int, int> OnPlayerSpawned_HPUIManager;

    //PlayerHitBox에서 Hit되었을 때
    public static event Action<Vector2, int> OnPlayerHitBoxHitted_PlayerDamageHandler;

    //PlayerDamageHandler에서 데미지 처리할 때
    public static event Action<int, int> OnPlayerDamaged_HPUIManager;

    //Player에서 피격 전용 무적시간 종료되었을 때
    public static event Action OnPlayerKnockedBackInvincibleOver;

    //PlayerParry가 성공했을 때
    public static event Action OnPlayerParrySuccess;

    //Player가 사망했을 때
    public static event Action OnPlayerDead;



    //Player의 Start()에서 이벤트 발행
    public static void InvokePlayerInstance(Player player)
    {
        OnPlayerInstance?.Invoke(player);
    }
    //Player의 Start()에서 이벤트 발행
    public static void InvokePlayerSpawned_HPUIManager(int maxHP, int currentHP)
    {
        OnPlayerSpawned_HPUIManager?.Invoke(maxHP, currentHP);
    }
    //PlayerHitBox에서 이벤트 발행
    public static void InvokePlayerHitBoxHitted(Vector2 hitTargetPos, int damage)
    {
        OnPlayerHitBoxHitted_PlayerDamageHandler?.Invoke(hitTargetPos, damage);
    }
    //PlayerDamageHandler에서 이벤트 발행
    public static void InvokePlayerDamaged(int maxHp, int currentHp)
    {
        OnPlayerDamaged_HPUIManager?.Invoke(maxHp, currentHp);
    }
    //Player에서 피격 전용 무적시간 종료되었을 때
    public static void InvokePlayerKnockedBackInvincibleOver()
    {
        OnPlayerKnockedBackInvincibleOver?.Invoke();
    }
    //PlayerParry가 성공했을 때
    public static void InvokePlayerParrySuccess()
    {
        OnPlayerParrySuccess?.Invoke();
    }
    //Player가 사망했을 때
    public static void InvokePlayerDead()
    {
        OnPlayerDead?.Invoke();
    }
}
