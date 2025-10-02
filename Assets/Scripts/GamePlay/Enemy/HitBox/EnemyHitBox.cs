using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour, IDamageable
{
    //IDamageable 인터페이스로 실행되는 메소드
    public void TakeDamage(Vector2 hitTargetPos, int damage)
    {
        EnemyEvents.InvokeEnemyHitBoxHitted(hitTargetPos, damage);

    }

}
