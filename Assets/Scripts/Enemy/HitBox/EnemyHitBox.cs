using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour, IDamageable
{
    //IDamageable �������̽��� ����Ǵ� �޼ҵ�
    public void TakeDamage(Vector2 hitTargetPos, int damage)
    {
        EnemyEvents.InvokeEnemyHitBoxHitted(hitTargetPos, damage);

    }

}
