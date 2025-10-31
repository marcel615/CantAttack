using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PlayerEvents
{
    //PlayerController가 스폰될 때
    public static event Action<PlayerController> OnPlayerControllerInstance;

    public static event Action<int, int> OnPlayerSpawned_HPUIManager;

    //PlayerHitBox에서 Hit되었을 때
    public static event Action<Vector2, int> OnPlayerHitBoxHitted_PlayerDamageHandler;

    //PlayerDamageHandler에서 데미지 처리할 때
    public static event Action<int, int> OnPlayerDamaged_HPUIManager;

    //Player에서 피격 전용 무적시간 종료되었을 때
    public static event Action OnPlayerKnockedBackInvincibleOver;

    //PlayerParry가 성공했을 때
    public static event Action OnPlayerParrySuccess;

    //PlayerParry가 투사체 패링에 성공했을 때
    public static event Action<ProjectileBase, GameObject> OnProjectileParried;

    //Player가 사망했을 때
    public static event Action OnPlayerDead;

    //DoubleJump 기술 해금될 때
    public static event Action OnDoubleJumpUnlock;

    //PlayerShieldSlot이 업데이트되었을 때
    public static event Action<ShieldDataSO[]> OnShieldSlotUpdated;

    //ShieldSlot 변경 요청이 들어올 때
    public static event Action<int> OnShieldSlotSelected;

    //currentShield가 변경되었을 때
    public static event Action<ShieldDataSO, int> OnCurrentShieldUpdated;

    //Shield 인벤토리에서 슬롯 교체 요청할 때
    public static event Action<ShieldDataSO, int> OnRequestEquipShield;


    //ParryModeSlot이 업데이트되었을 때
    public static event Action<ParryModeDataSO[]> OnParryModeSlotUpdated;

    //ParryModeSlot 변경 요청이 들어올 때
    public static event Action<int> OnParryModeSlotSelected;

    //currentParryMode가 변경되었을 때
    public static event Action<ParryModeDataSO, int> OnCurrentParryModeUpdated;

    //ParryMode 인벤토리에서 슬롯 교체 요청할 때
    //public static event Action<ParryModeDataSO, int> OnRequestEquipParryMode;


    //parryAction의 방향이 정해졌을 때
    public static event Action<Vector2> OnParryActionDirectionSet;

    //parryAction의 타겟이 정해졌을 때
    public static event Action<GameObject> OnParryActionTargetSet;




    //PlayerController가 스폰될 때
    public static void InvokePlayerControllerInstance(PlayerController player)
    {
        OnPlayerControllerInstance?.Invoke(player);
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
    //PlayerParry가 투사체 패링에 성공했을 때
    public static void InvokeProjectileParried(ProjectileBase prefab, GameObject sender)
    {
        OnProjectileParried?.Invoke(prefab, sender);
    }
    //Player가 사망했을 때
    public static void InvokePlayerDead()
    {
        OnPlayerDead?.Invoke();
    }
    //DoubleJump 기술 해금될 때
    public static void InvokeDoubleJumpUnlock()
    {
        OnDoubleJumpUnlock?.Invoke();
    }
    //PlayerShieldSlot이 업데이트되었을 때
    public static void InvokeShieldSlotUpdated(ShieldDataSO[] slots)
    {
        OnShieldSlotUpdated?.Invoke(slots);
    }
    //ShieldSlot 변경 요청이 들어올 때
    public static void InvokeShieldSlotSelected(int index)
    {
        OnShieldSlotSelected?.Invoke(index);
    }
    //currentShield가 변경되었을 때
    public static void InvokeCurrentShieldUpdated(ShieldDataSO currentShield, int currentIndex)
    {
        OnCurrentShieldUpdated?.Invoke(currentShield, currentIndex);
    }
    //currentShield가 변경되었을 때
    public static void InvokeRequestEquipShield(ShieldDataSO shieldData, int index)
    {
        OnRequestEquipShield?.Invoke(shieldData, index);
    }

    //ParryModeSlot이 업데이트되었을 때
    public static void InvokeParryModeSlotUpdated(ParryModeDataSO[] slots)
    {
        OnParryModeSlotUpdated?.Invoke(slots);
    }
    //ParryModeSlot 변경 요청이 들어올 때
    public static void InvokeParryModeSlotSelected(int index)
    {
        OnParryModeSlotSelected?.Invoke(index);
    }
    //currentParryMode가 변경되었을 때
    public static void InvokeCurrentParryModeUpdated(ParryModeDataSO currentParryMode, int currentIndex)
    {
        OnCurrentParryModeUpdated?.Invoke(currentParryMode, currentIndex);
    }
    //currentParryMode가 변경되었을 때
    /*
    public static void InvokeRequestEquipParryMode(ParryModeDataSO parryModeData, int index)
    {
        OnRequestEquipParryMode?.Invoke(parryModeData, index);
    }
    */

    //parryAction의 방향이 정해졌을 때
    public static void InvokeParryActionDirectionSet(Vector2 dir)
    {
        OnParryActionDirectionSet?.Invoke(dir);
    }
    //parryAction의 타겟이 정해졌을 때
    public static void InvokeParryActionTargetSet(GameObject gameObject)
    {
        OnParryActionTargetSet?.Invoke(gameObject);
    }


}
