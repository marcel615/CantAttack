using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAIdleState : EnemyState
{
    //내 컴포넌트
    Rigidbody2D rigid;

    //순찰 관련 변수
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
        //시작 방향 초기화
        startPos = transform.position;
        //무작위 방향 선택(-1 or 1)
        patrolDir = Random.Range(0, 2) == 0 ? -1 : 1;
        //무작위 순찰 거리 선택
        patrolDistance = Random.Range(1f, patrolMaxDistance);
        //순찰 거리 추적 변수 초기화
        moveDistance = 0;
        //순찰 한 번 돌고 잠시 멈출 시간 
        waitTime = Random.Range(3f, 6f);

    }
    public override void UpdateState()
    {
        if (isWaiting) return;

        //순찰 거리 추적 변수 업데이트
        moveDistance = Mathf.Abs(transform.position.x - startPos.x);

        //순찰 거리가 충족되기 전까지 계속 순찰돌도록 구현
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
