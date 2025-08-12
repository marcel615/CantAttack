using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    public override void OnDamaged(Vector2 hitTargetPos, int damage)
    {
        base.OnDamaged(hitTargetPos, damage);

        if (CurrentHP <= 0)
        {
            GameEvents.InvokeBossFightEnd();
        }
    }

}
