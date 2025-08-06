using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAIdleState : EnemyState
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    Animator animator;

    //���� ���� Controller ����
    float patrolSpeed;
    float patrolMinDistance;
    float patrolMaxDistance;
    float patrolMinWaitTime;
    float patrolMaxWaitTime;

    //���� ���� ����
    public int patrolDir;
    public float patrolDistance;
    public float patrolWaitTime;
    private Vector2 startPos;
    public float moveDistance;
    bool isWaiting;

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
        patrolMinWaitTime = FSM.enemyController.patrolMinWaitTime;
        patrolMaxWaitTime = FSM.enemyController.patrolMaxWaitTime;

        //���� �ʱ�ȭ
        patrolDir = Random.Range(0, 2) == 0 ? -1 : 1; //������ ���� ����(-1 or 1)
        patrolDistance = Random.Range(patrolMinDistance, patrolMaxDistance); //������ ���� �Ÿ� ����
        patrolWaitTime = Random.Range(patrolMinWaitTime, patrolMaxWaitTime);    //���� �� �� ���� ��� ���� �ð� 
        startPos = transform.position;  //���� ���� �ʱ�ȭ
        moveDistance = 0;   //���� �Ÿ� ���� ���� �ʱ�ȭ

        //StartCoroutine(WaitBeforeStart());
    }
    public override void UpdateState()
    {
        //��ٸ��� �ð����� �ٷ� ����
        if (isWaiting) return;
        //�˹鵿���� ���� ���ϵ���
        if (FSM.enemyController.isKnockbacked) return;
        //���Ͻð������� ���� ���ϵ���
        if (FSM.enemyController.isParryStun) return;

        //�տ� ���� ���ų� �տ� ���� ������ ���� ��ȯ, ���� �Ÿ�, startPos �缳��
        if (!FSM.enemyController.isGroundFront() || FSM.enemyController.isWallFront())
        {
            patrolDistance -= moveDistance;
            patrolDir *= -1;
            startPos = transform.position;

            //patrolDir�� ���� ĳ���� �¿� ����
            transform.localScale = new Vector3(patrolDir, 1, 1);
            FSM.enemyController.isHeadToRight = (patrolDir > 0) ? 1 : -1; //patrolDir�� ����� 1 ����, ������ -1 ����
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
            else
            {
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                StartCoroutine(WaitAndResume());
            }
        }
    }
    public override void Exit()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        animator.SetBool("isMoving", false);
    }

    private IEnumerator WaitBeforeStart()
    {
        isWaiting = true;
        yield return new WaitForSeconds(patrolWaitTime);

        isWaiting = false;
    }
    private IEnumerator WaitAndResume()
    {
        isWaiting = true;
        animator.SetBool("isMoving", false);

        yield return new WaitForSeconds(patrolWaitTime);

        isWaiting = false;
        FSM.ChangeState(FSM.idleState);
    }
}
