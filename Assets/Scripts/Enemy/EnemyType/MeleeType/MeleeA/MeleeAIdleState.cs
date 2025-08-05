using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAIdleState : EnemyState
{
    //�� ������Ʈ
    Rigidbody2D rigid;

    //���� ���� ����
    private Vector2 startPos;
    public int patrolDir;
    public float patrolDistance;
    public float patrolMaxDistance;
    public float moveDistance;
    public float waitTime;
    bool isWaiting;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public override void Enter()
    {
        //���� ���� �ʱ�ȭ
        startPos = transform.position;
        //������ ���� ����(-1 or 1)
        patrolDir = Random.Range(0, 2) == 0 ? -1 : 1;
        //������ ���� �Ÿ� ����
        patrolDistance = Random.Range(1f, patrolMaxDistance);
        //���� �Ÿ� ���� ���� �ʱ�ȭ
        moveDistance = 0;
        //���� �� �� ���� ��� ���� �ð� 
        waitTime = Random.Range(3f, 6f);

    }
    public override void UpdateState()
    {
        if (isWaiting) return;

        //���� �Ÿ� ���� ���� ������Ʈ
        moveDistance = Mathf.Abs(transform.position.x - startPos.x);

        //���� �Ÿ��� �����Ǳ� ������ ��� ���������� ����
        if (moveDistance < patrolDistance)
        {
            rigid.velocity = new Vector2(patrolDir * FSM.enemyController.patrolSpeed, rigid.velocity.y);
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

    private IEnumerator WaitAndResume()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        isWaiting = false;
        FSM.ChangeState(FSM.idleState);
    }
}
