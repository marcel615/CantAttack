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
    float attackMinWaitTime;
    float attackMaxWaitTime;
    public float attackTime;

    //공격 관련 변수
    public GameObject slashEffectPrefab;
    public float attackWaitTime;
    bool isAttacking;
    bool isWaiting;



    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        //Controller 변수 초기화
        attackMinWaitTime = FSM.enemyController.attackMinWaitTime;
        attackMaxWaitTime = FSM.enemyController.attackMaxWaitTime;
        attackTime = FSM.enemyController.attackTime;

        //변수 초기화
        attackWaitTime = Random.Range(attackMinWaitTime, attackMaxWaitTime);    //공격 한 번 하고 잠시 멈출 시간 
    }
    public override void UpdateState()
    {
        //공격하는 시간에는 바로 종료
        if (isAttacking) return;
        //기다리는 시간에는 바로 종료
        if (isWaiting) return;
        //공격 트리거에서 벗어나면 ChaseState로 전환 후 종료
        if (!FSM.enemyController.isAttackEnable)
        {
            FSM.ChangeState(FSM.chaseState);
            return;
        }
        
        //공격 시작
        StartCoroutine(AttackAndWaitBeforeStart(attackTime));

    }
    public override void Exit()
    {
    }
    private IEnumerator AttackAndWaitBeforeStart(float attackTime)
    {
        //공격 시작 플래그
        isAttacking = true;

        //공격 이펙트 생성
        Instantiate(slashEffectPrefab, MeleeAttack1Point.position, Quaternion.identity, MeleeAttack1Point);
        //공격 콜라이더 활성화
        MeleeAttack1Point.GetComponent<BoxCollider2D>().enabled = true;
        //공격 시작 시 속도 멈추도록
        rigid.velocity = new Vector2(0, rigid.velocity.y);

        //공격 시간 후
        yield return new WaitForSeconds(attackTime);

        //공격 종료 플래그
        isAttacking = false;

        //공격 콜라이더 비활성화
        MeleeAttack1Point.GetComponent<BoxCollider2D>().enabled = false;

        //Wait 시작 플래그
        isWaiting = true;
        yield return new WaitForSeconds(attackWaitTime);

        //Wait 종료 플래그
        isWaiting = false;
    }
}
