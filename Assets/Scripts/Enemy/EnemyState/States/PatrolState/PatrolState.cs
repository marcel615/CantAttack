using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;

    //순찰 관련 Controller 변수
    float patrolSpeed;
    float patrolMinDistance;
    float patrolMaxDistance;

    //순찰 관련 변수
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
        //Controller 변수 초기화
        patrolSpeed = FSM.enemyController.patrolSpeed;
        patrolMinDistance = FSM.enemyController.patrolMinDistance;
        patrolMaxDistance = FSM.enemyController.patrolMaxDistance;

        //변수 초기화
        patrolDir = Random.Range(0, 2) == 0 ? -1 : 1; //무작위 방향 선택(-1 or 1)
        patrolDistance = Random.Range(patrolMinDistance, patrolMaxDistance); //무작위 순찰 거리 선택
        startPos = transform.position;  //시작 방향 초기화
        moveDistance = 0;   //순찰 거리 추적 변수 초기화
    }

    public override void UpdateState()
    {
        //넉백동안은 실행 안하도록
        if (FSM.enemyController.isKnockbacked) return;

        //앞에 땅이 없거나 앞에 벽이 있으면 방향 전환, 순찰 거리, startPos 재설정
        if (!FSM.enemyController.isGroundFront() || FSM.enemyController.isWallFront())
        {
            patrolDistance -= moveDistance;
            patrolDir *= -1;
            startPos = transform.position;

            //patrolDir에 따라 캐릭터 좌우 반전
            transform.localScale = new Vector3(patrolDir, 1, 1);
            FSM.enemyController.isHeadToRight = (patrolDir > 0) ? 1 : -1; //patrolDir가 양수면 1 저장, 음수면 -1 저장
            animator.SetBool("isMoving", true);
        }
        else
        {
            //순찰 거리 추적 변수 업데이트
            moveDistance = Mathf.Abs(transform.position.x - startPos.x);

            //순찰 거리가 충족되기 전까지 계속 순찰돌도록 구현
            if (moveDistance < patrolDistance)
            {
                rigid.velocity = new Vector2(patrolDir * patrolSpeed, rigid.velocity.y);

                //patrolDir에 따라 캐릭터 좌우 반전
                transform.localScale = new Vector3(patrolDir, 1, 1);
                FSM.enemyController.isHeadToRight = (patrolDir > 0) ? 1 : -1; //patrolDir가 양수면 1 저장, 음수면 -1 저장
                animator.SetBool("isMoving", true);
            }
            else //순찰 종료
            {
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                animator.SetBool("isMoving", false);

                //다음 상태로 전환되는지 체크하고 전환하도록
                if (FSM.CanChangeState(FSM.idleState))
                    FSM.ChangeState(FSM.idleState);
            }
        }

    }
    public override void Exit()
    {

    }
}
