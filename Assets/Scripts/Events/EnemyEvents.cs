using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyEvents
{
    //EnemyHitBox에서 Hit되었을 때
    public static event Action<Vector2, int> OnEnemyHitBoxHitted_EnemyDamageHandler;

    //EnemyPlayerDetector에서 플레이어를 감지했을 때
    public static event Action<GameObject> OnEnemyPlayerDetected;
    //EnemyChaseRange에서 플레이어가 감지에서 Exit했을 때
    public static event Action<GameObject> OnEnemyChaseOver;

    //EnemyAttackTrigger에서 플레이어를 감지했을 때
    public static event Action<GameObject> OnEnemyAttackTriggerEnter;
    //EnemyAttackTrigger에서 플레이어가 감지에서 Exit했을 때
    public static event Action<GameObject> OnEnemyAttackTriggerExit;


    //EnemyHitBox에서 Hit되었을 때
    public static void InvokeEnemyHitBoxHitted(Vector2 hitTargetPos, int damage)
    {
        OnEnemyHitBoxHitted_EnemyDamageHandler?.Invoke(hitTargetPos, damage);
    }

    //EnemyPlayerDetector에서 플레이어를 감지했을 때
    public static void InvokeEnemyPlayerDetected(GameObject player)
    {
        OnEnemyPlayerDetected?.Invoke(player);
    }
    //EnemyChaseRange에서 플레이어가 감지에서 Exit했을 때
    public static void InvokeEnemyChaseOver(GameObject player)
    {
        OnEnemyChaseOver?.Invoke(player);
    }

    //EnemyAttackTrigger에서 플레이어를 감지했을 때
    public static void InvokeEnemyAttackTriggerEnter(GameObject player)
    {
        OnEnemyAttackTriggerEnter?.Invoke(player);
    }
    //EnemyAttackTrigger에서 플레이어가 감지에서 Exit했을 때
    public static void InvokeEnemyAttackTriggerExit(GameObject player)
    {
        OnEnemyAttackTriggerExit?.Invoke(player);
    }

}
