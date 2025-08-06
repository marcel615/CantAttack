using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyEvents
{
    //EnemyHitBox���� Hit�Ǿ��� ��
    public static event Action<Vector2, int> OnEnemyHitBoxHitted_EnemyDamageHandler;

    //EnemyPlayerDetector���� �÷��̾ �������� ��
    public static event Action<GameObject> OnEnemyPlayerDetected;
    //EnemyChaseRange���� �÷��̾ �������� Exit���� ��
    public static event Action<GameObject> OnEnemyChaseOver;

    //EnemyAttackTrigger���� �÷��̾ �������� ��
    public static event Action<GameObject> OnEnemyAttackTriggerEnter;
    //EnemyAttackTrigger���� �÷��̾ �������� Exit���� ��
    public static event Action<GameObject> OnEnemyAttackTriggerExit;


    //EnemyHitBox���� Hit�Ǿ��� ��
    public static void InvokeEnemyHitBoxHitted(Vector2 hitTargetPos, int damage)
    {
        OnEnemyHitBoxHitted_EnemyDamageHandler?.Invoke(hitTargetPos, damage);
    }

    //EnemyPlayerDetector���� �÷��̾ �������� ��
    public static void InvokeEnemyPlayerDetected(GameObject player)
    {
        OnEnemyPlayerDetected?.Invoke(player);
    }
    //EnemyChaseRange���� �÷��̾ �������� Exit���� ��
    public static void InvokeEnemyChaseOver(GameObject player)
    {
        OnEnemyChaseOver?.Invoke(player);
    }

    //EnemyAttackTrigger���� �÷��̾ �������� ��
    public static void InvokeEnemyAttackTriggerEnter(GameObject player)
    {
        OnEnemyAttackTriggerEnter?.Invoke(player);
    }
    //EnemyAttackTrigger���� �÷��̾ �������� Exit���� ��
    public static void InvokeEnemyAttackTriggerExit(GameObject player)
    {
        OnEnemyAttackTriggerExit?.Invoke(player);
    }

}
