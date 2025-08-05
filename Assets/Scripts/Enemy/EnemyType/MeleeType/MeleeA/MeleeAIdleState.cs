using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAIdleState : EnemyState
{
    //�� ������Ʈ
    Rigidbody2D rigid;

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

    }
    public override void UpdateState()
    {
        if (isWaiting) return;

        //���� �Ÿ� ���� ���� ������Ʈ
        moveDistance = Mathf.Abs(transform.position.x - startPos.x);

        //���� �Ÿ��� �����Ǳ� ������ ��� ���������� ����
        if (moveDistance < patrolDistance)
        {
            Debug.Log(patrolDir);
            rigid.velocity = new Vector2(patrolDir * patrolSpeed, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = new Vector2(0f, rigid.velocity.y);
            StartCoroutine(WaitAndResume());
        }
    }
    public override void Exit()
    {
        rigid.velocity = Vector2.zero;
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
        yield return new WaitForSeconds(patrolWaitTime);

        isWaiting = false;
        FSM.ChangeState(FSM.idleState);
    }
}
