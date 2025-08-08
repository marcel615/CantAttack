using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    Animator animator;

    //idle ���� Controller ����
    float idleMinWaitTime;
    float idleMaxWaitTime;

    //idle ���� ����
    float idleTime;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        //Controller ���� �ʱ�ȭ
        idleMinWaitTime = FSM.enemyController.idleMinWaitTime;
        idleMaxWaitTime = FSM.enemyController.idleMaxWaitTime;

        //���� �ʱ�ȭ
        idleTime = Random.Range(idleMinWaitTime, idleMaxWaitTime);    //idle �ϰ��ִ� �ð�
    }
    public override void UpdateState()
    {
        //�˹鵿���� ���� ���ϵ���
        if (FSM.enemyController.isKnockbacked) return;
        
        //������ �ð���ŭ idle�ϵ��� ����
        if(idleTime > 0)
        {
            idleTime -= Time.deltaTime;

            rigid.velocity = new Vector2(0, rigid.velocity.y);
            animator.SetBool("isMoving", false);
        }
        else
        {
            idleTime = 0;

            //���� ���·� ��ȯ�Ǵ��� üũ�ϰ� ��ȯ�ϵ���
            if (FSM.CanChangeState(FSM.patrolState))
                FSM.ChangeState(FSM.patrolState);
        }
        
    }
    public override void Exit()
    {
    }

}
