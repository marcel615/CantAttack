using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitBox : MonoBehaviour, IParryable
{
    [SerializeField] private EnemyFSM FSM;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(transform.position, FSM.enemyController.attackDamage);
        }
    }
    public void OnParried(GameObject player)
    {
        EnemyEvents.InvokeEnemyAttackParried();
    }

}
