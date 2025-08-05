using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAIdleState : EnemyState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    //순찰 관련 Controller 변수
    float patrolSpeed;
    float patrolMinDistance;
    float patrolMaxDistance;
    float patrolMinWaitTime;
    float patrolMaxWaitTime;

    //순찰 관련 변수
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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void Enter()
    {
        //Controller 변수 초기화
        patrolSpeed = FSM.enemyController.patrolSpeed;
        patrolMinDistance = FSM.enemyController.patrolMinDistance;
        patrolMaxDistance = FSM.enemyController.patrolMaxDistance;
        patrolMinWaitTime = FSM.enemyController.patrolMinWaitTime;
        patrolMaxWaitTime = FSM.enemyController.patrolMaxWaitTime;

        //변수 초기화
        patrolDir = Random.Range(0, 2) == 0 ? -1 : 1; //무작위 방향 선택(-1 or 1)
        patrolDistance = Random.Range(patrolMinDistance, patrolMaxDistance); //무작위 순찰 거리 선택
        patrolWaitTime = Random.Range(patrolMinWaitTime, patrolMaxWaitTime);    //순찰 한 번 돌고 잠시 멈출 시간 
        startPos = transform.position;  //시작 방향 초기화
        moveDistance = 0;   //순찰 거리 추적 변수 초기화

        //StartCoroutine(WaitBeforeStart());
    }
    public override void UpdateState()
    {
        if (isWaiting) return;

        //순찰 거리 추적 변수 업데이트
        moveDistance = Mathf.Abs(transform.position.x - startPos.x);

        //순찰 거리가 충족되기 전까지 계속 순찰돌도록 구현
        if (moveDistance < patrolDistance)
        {
            rigid.velocity = new Vector2(patrolDir * patrolSpeed, rigid.velocity.y);

            spriteRenderer.flipX = patrolDir < 0;
            animator.SetBool("isMoving", true);
            FSM.enemyController.isHeadToRight = (patrolDir > 0) ? 1 : -1; //patrolDir가 양수면 1 저장, 음수면 -1 저장
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
