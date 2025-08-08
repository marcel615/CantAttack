using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;

    //공격 관련 Controller 변수
    float attackTime;
    float attackWaitTime;
    Transform MeleeAttack1Point;
    GameObject slashEffectPrefab;

    //공격 관련 변수
    Coroutine attackCoroutine;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        //Controller 변수 초기화
        attackTime = FSM.enemyController.attackTime;
        attackWaitTime = FSM.enemyController.attackWaitTime;
        MeleeAttack1Point = FSM.enemyController.MeleeAttack1Point.transform;
        slashEffectPrefab = FSM.enemyController.slashEffectPrefab;

        //변수 초기화

        //공격 시작
        attackCoroutine = StartCoroutine(AttackAndWaitBeforeStart(attackTime));
    }
    public override void UpdateState()
    {
    }
    public override void Exit()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        //공격 콜라이더 비활성화
        DisableAttackHitBox();
    }

    private IEnumerator AttackAndWaitBeforeStart(float attackTime)
    {
        //애니메이션 재생, EnableAttackHitBox() 애니메이션 이벤트로 실행
        animator.SetTrigger("isAttack");

        //공격 시작 시 이동 멈추도록
        rigid.velocity = new Vector2(0, rigid.velocity.y);

        //공격 시간 후
        yield return new WaitForSeconds(attackTime);

        //공격 콜라이더 비활성화 
        DisableAttackHitBox();  //애니메이션 이벤트로 호출 안될 시 대비

        yield return new WaitForSeconds(attackWaitTime);

        //다음 상태로 전환되도록
        EnemyState nextState = FSM.DecideNextState();
        FSM.ChangeState(nextState);
    }

    //애니메이션에서 애니메이션 이벤트로 호출됨
    public void EnableAttackHitBox()
    {
        //공격 이펙트 생성
        Instantiate(slashEffectPrefab, MeleeAttack1Point.position, Quaternion.identity, MeleeAttack1Point);
        //공격 콜라이더 활성화
        MeleeAttack1Point.GetComponent<BoxCollider2D>().enabled = true;
    }
    //애니메이션에서 애니메이션 이벤트로 호출됨
    public void DisableAttackHitBox()
    {
        //공격 콜라이더 비활성화
        MeleeAttack1Point.GetComponent<BoxCollider2D>().enabled = false;
    }
}
