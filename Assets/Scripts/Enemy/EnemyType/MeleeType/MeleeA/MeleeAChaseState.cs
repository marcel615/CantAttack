using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAChaseState : EnemyState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;

    //Chase 관련 Controller 변수
    GameObject player;
    float chaseSpeed;

    //Chase 관련 변수
    int chaseDir;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        //Controller 변수 초기화
        player = FSM.enemyController.player;
        chaseSpeed = FSM.enemyController.chaseSpeed;
    }

    public override void UpdateState()
    {
        //넉백동안은 실행 안하도록
        if (FSM.enemyController.isKnockbacked) return;
        //스턴시간동안은 실행 안하도록
        if (FSM.enemyController.isParryStun) return;

        //chase 방향 설정
        chaseDir = (player.transform.position.x > transform.position.x) ? 1 : -1;

        //chaseDir 따라 캐릭터 좌우 반전
        transform.localScale = new Vector3(chaseDir, 1, 1);
        FSM.enemyController.isHeadToRight = (chaseDir > 0) ? 1 : -1; //chaseDir가 양수면 1 저장, 음수면 -1 저장

        //앞에 땅이 없거나 앞에 벽이 있으면 chase 상태이긴 하지만 멈추도록
        if (!FSM.enemyController.isGroundFront() || FSM.enemyController.isWallFront())
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            animator.SetBool("isMoving", false);
        }
        else
        {
            rigid.velocity = new Vector2(chaseDir * chaseSpeed, rigid.velocity.y);
            animator.SetBool("isMoving", true);
        }

    }

    public override void Exit()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        animator.SetBool("isMoving", false);
    }
}
