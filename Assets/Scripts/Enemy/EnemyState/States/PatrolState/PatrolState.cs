using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    Animator animator;

    //���� ���� Controller ����
    float patrolSpeed;
    float patrolMinDistance;
    float patrolMaxDistance;

    //���� ���� ����
    int patrolDir;
    float patrolDistance;
    Vector2 startPos;
    float moveDistance;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        //Controller ���� �ʱ�ȭ
        patrolSpeed = FSM.enemyController.patrolSpeed;
        patrolMinDistance = FSM.enemyController.patrolMinDistance;
        patrolMaxDistance = FSM.enemyController.patrolMaxDistance;

        //���� �ʱ�ȭ
        patrolDir = Random.Range(0, 2) == 0 ? -1 : 1; //������ ���� ����(-1 or 1)
        patrolDistance = Random.Range(patrolMinDistance, patrolMaxDistance); //������ ���� �Ÿ� ����
        startPos = transform.position;  //���� ���� �ʱ�ȭ
        moveDistance = 0;   //���� �Ÿ� ���� ���� �ʱ�ȭ
    }

    public override void UpdateState()
    {
        //�˹鵿���� ���� ���ϵ���
        if (FSM.enemyController.isKnockbacked) return;

        //�տ� ���� ���ų� �տ� ���� ������ ���� ��ȯ, ���� �Ÿ�, startPos �缳��
        if (!FSM.enemyController.isGroundFront() || FSM.enemyController.isWallFront())
        {
            patrolDistance -= moveDistance;
            patrolDir *= -1;
            startPos = transform.position;

            //patrolDir�� ���� ĳ���� �¿� ����
            transform.localScale = new Vector3(patrolDir, 1, 1);
            FSM.enemyController.isHeadToRight = (patrolDir > 0) ? 1 : -1; //patrolDir�� ����� 1 ����, ������ -1 ����
            animator.SetBool("isMoving", true);
        }
        else
        {
            //���� �Ÿ� ���� ���� ������Ʈ
            moveDistance = Mathf.Abs(transform.position.x - startPos.x);

            //���� �Ÿ��� �����Ǳ� ������ ��� ���������� ����
            if (moveDistance < patrolDistance)
            {
                rigid.velocity = new Vector2(patrolDir * patrolSpeed, rigid.velocity.y);

                //patrolDir�� ���� ĳ���� �¿� ����
                transform.localScale = new Vector3(patrolDir, 1, 1);
                FSM.enemyController.isHeadToRight = (patrolDir > 0) ? 1 : -1; //patrolDir�� ����� 1 ����, ������ -1 ����
                animator.SetBool("isMoving", true);
            }
            else //���� ����
            {
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                animator.SetBool("isMoving", false);

                //���� ���·� ��ȯ�Ǵ��� üũ�ϰ� ��ȯ�ϵ���
                if (FSM.CanChangeState(FSM.idleState))
                    FSM.ChangeState(FSM.idleState);
            }
        }

    }
    public override void Exit()
    {

    }
}
