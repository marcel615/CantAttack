using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : EnemyState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;

    //내 자식 오브젝트

    //공격 관련 Controller 변수
    float parryStunTime;

    //공격 관련 변수
    Coroutine stunCoroutine;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        //Controller 변수 초기화
        parryStunTime = FSM.enemyController.parryStunTime;

        //변수 초기화

        //ParryStun 코루틴 실행
        stunCoroutine = StartCoroutine(ParryStun());
    }

    public override void UpdateState()
    {
    }

    public override void Exit()
    {
        if (stunCoroutine != null)
        {
            StopCoroutine(stunCoroutine);
        }

        //패리로 인한 스턴 플래그 false로
        FSM.enemyController.isParryStun = false;
        //애니메이션 재생 종료
        animator.SetBool("isStun", false);

    }
    private IEnumerator ParryStun()
    {
        //패리로 인한 스턴 플래그 true로
        FSM.enemyController.isParryStun = true;

        //애니메이션 재생
        animator.SetBool("isStun", true);

        //이동 멈추도록
        rigid.velocity = new Vector2(0, rigid.velocity.y);

        //스턴 시간 기다리고
        yield return new WaitForSeconds(parryStunTime);


        //패리로 인한 스턴 플래그 false로
        FSM.enemyController.isParryStun = false;

        //애니메이션 재생 종료
        animator.SetBool("isStun", false);

        //다음 상태로 전환되도록
        EnemyState nextState = FSM.DecideNextState();
        FSM.ChangeState(nextState);
    }
}
