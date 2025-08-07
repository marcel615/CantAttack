using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAAttackState : EnemyState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;

    //내 자식 오브젝트
    public Transform MeleeAttack1Point;

    //공격 관련 Controller 변수
    public float attackTime;
    public float attackWaitTime;

    //공격 관련 변수
    public GameObject slashEffectPrefab;
    public bool isAttacking;
    public bool isWaiting;
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

        //변수 초기화
    }
    public override void UpdateState()
    {
        //공격하는 시간에는 바로 종료
        if (isAttacking) return;
        //기다리는 시간에는 바로 종료
        if (isWaiting) return;
        //스턴시간동안은 실행 안하도록
        if (FSM.enemyController.isParryStun) return;
        //공격 트리거에서 벗어나면 ChaseState로 전환 후 종료
        if (!FSM.enemyController.isAttackEnable)
        {
            Debug.Log("Test");
            FSM.ChangeState(FSM.chaseState);
            return;
        }
        
        //공격 시작
        attackCoroutine = StartCoroutine(AttackAndWaitBeforeStart(attackTime));

    }
    public override void Exit()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        //공격 콜라이더 비활성화
        DisableAttackHitBox();
        //공격 종료 플래그 초기화
        isAttacking = false;
        //Wait 종료 플래그 초기화
        isWaiting = false;
    }

    private IEnumerator AttackAndWaitBeforeStart(float attackTime)
    {
        //공격 시작 플래그
        isAttacking = true;

        //애니메이션 재생, EnableAttackHitBox() 애니메이션 이벤트로 실행
        animator.SetTrigger("isAttack");

        //공격 시작 시 이동 멈추도록
        rigid.velocity = new Vector2(0, rigid.velocity.y);

        //공격 시간 후
        yield return new WaitForSeconds(attackTime);

        //공격 종료 플래그
        isAttacking = false;

        //공격 콜라이더 비활성화 
        DisableAttackHitBox();  //애니메이션 이벤트로 호출 안될 시 대비

        //Wait 시작 플래그
        isWaiting = true;

        yield return new WaitForSeconds(attackWaitTime);

        //Wait 종료 플래그
        isWaiting = false;

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
