using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour, IDamageable
{
    //�θ� ������Ʈ
    [SerializeField] private EnemyBehavior enemyBehavior;

    private void Awake()
    {
        //������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (enemyBehavior == null) enemyBehavior = GetComponentInParent<EnemyBehavior>();
    }

    //IDamageable �������̽��� ����Ǵ� �޼ҵ�
    public void TakeDamage(Vector2 hitTargetPos, int damage)
    {
        EnemyEvents.InvokeEnemyHitBoxHitted(hitTargetPos, damage);

    }

}
