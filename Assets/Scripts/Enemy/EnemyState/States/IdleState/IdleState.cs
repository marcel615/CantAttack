using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;

    //idle 관련 Controller 변수
    float idleMinWaitTime;
    float idleMaxWaitTime;

    //idle 관련 변수
    float idleTime;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        //Controller 변수 초기화
        idleMinWaitTime = FSM.enemyController.idleMinWaitTime;
        idleMaxWaitTime = FSM.enemyController.idleMaxWaitTime;

        //변수 초기화
        idleTime = Random.Range(idleMinWaitTime, idleMaxWaitTime);    //idle 하고있는 시간
    }
    public override void UpdateState()
    {
        //넉백동안은 실행 안하도록
        if (FSM.enemyController.isKnockbacked) return;
        
        //정해진 시간만큼 idle하도록 구현
        if(idleTime > 0)
        {
            idleTime -= Time.deltaTime;

            rigid.velocity = new Vector2(0, rigid.velocity.y);
            animator.SetBool("isMoving", false);
        }
        else
        {
            idleTime = 0;

            //다음 상태로 전환되는지 체크하고 전환하도록
            if (FSM.CanChangeState(FSM.patrolState))
                FSM.ChangeState(FSM.patrolState);
        }
        
    }
    public override void Exit()
    {
    }

}
