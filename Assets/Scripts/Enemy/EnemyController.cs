using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //�� ������Ʈ
    public EnemyFSM FSM;
    public EnemyReactionHandler reactionHandler;

    //Enemy �⺻ ������
    public int MaxHP = 10;
    public int CurrentHP;
    public float patrolSpeed;
    public float chaseSpeed;

    //Enemy �⺻ �÷���
    public bool isKnockbackEnable;



    private void Awake()
    {
        FSM = GetComponent<EnemyFSM>();
        reactionHandler = GetComponent<EnemyReactionHandler>();

        CurrentHP = MaxHP;
        isKnockbackEnable = true;
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

        //���� ü�¿� ���� �˹� Ȥ�� FSM State ��ȯ
        if (CurrentHP > 0)
        {
            if (isKnockbackEnable)
            {
                reactionHandler.HitWithKnockback(hitTargetPos);
            }
            else
            {
                reactionHandler.HitWithoutKnockback();
            }
        }
        else
        {
            FSM.ChangeState(FSM.deadState);
        }
    }

}
