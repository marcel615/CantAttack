using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    //�� ������Ʈ
    protected EnemyBehavior enemyBehavior;

    private void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        EnemyEvents.OnEnemyHitBoxHitted_EnemyDamageHandler += OnDamaged;
    }
    private void OnDisable()
    {
        EnemyEvents.OnEnemyHitBoxHitted_EnemyDamageHandler -= OnDamaged;
    }
    //�ǰ� �̺�Ʈ �߻� ��
    void OnDamaged(Vector2 hitTargetPos, int damage)
    {
        //������ �����Ű��
        enemyBehavior.CurrentHP -= damage;

        //���� ü�¿� ���� Hit() Ȥ�� Dead() �����Ű��
        if (enemyBehavior.CurrentHP > 0)
            enemyBehavior.Hit(hitTargetPos);    
        else        
            enemyBehavior.Dead();
    }  

}
