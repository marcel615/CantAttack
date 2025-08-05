using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyEvents
{
    //EnemyHitBox���� Hit�Ǿ��� ��
    public static event Action<Vector2, int> OnEnemyHitBoxHitted_EnemyDamageHandler;


    //EnemyHitBox���� Hit�Ǿ��� ��
    public static void InvokeEnemyHitBoxHitted(Vector2 hitTargetPos, int damage)
    {
        OnEnemyHitBoxHitted_EnemyDamageHandler?.Invoke(hitTargetPos, damage);
    }

}
