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

}
