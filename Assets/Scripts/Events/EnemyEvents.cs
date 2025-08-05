using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyEvents
{
    //EnemyHitBox에서 Hit되었을 때
    public static event Action<Vector2, int> OnEnemyHitBoxHitted_EnemyDamageHandler;


    //EnemyHitBox에서 Hit되었을 때
    public static void InvokeEnemyHitBoxHitted(Vector2 hitTargetPos, int damage)
    {
        OnEnemyHitBoxHitted_EnemyDamageHandler?.Invoke(hitTargetPos, damage);
    }

}
