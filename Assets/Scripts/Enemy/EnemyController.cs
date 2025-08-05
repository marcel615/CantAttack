using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //�� ������Ʈ
    EnemyFSM FSM;

    //�ν����� �Ҵ� ������Ʈ
    public EnemyState hitState;
    public EnemyState deadState;

    public int MaxHP;
    public int CurrentHP;
    public float patrolSpeed;

    public void Init()
    {
        FSM = GetComponent<EnemyFSM>();
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
        CurrentHP -= damage;

        //���� ü�¿� ���� FSM�� State ����
        if (CurrentHP > 0)
            FSM.OnHit(hitTargetPos);
        else
            FSM.OnDead();
    }

}
