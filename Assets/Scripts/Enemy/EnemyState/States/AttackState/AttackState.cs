using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    Animator animator;

    //���� ���� Controller ����
    float attackTime;
    float attackWaitTime;
    Transform MeleeAttack1Point;
    GameObject slashEffectPrefab;

    //���� ���� ����
    Coroutine attackCoroutine;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        //Controller ���� �ʱ�ȭ
        attackTime = FSM.enemyController.attackTime;
        attackWaitTime = FSM.enemyController.attackWaitTime;
        MeleeAttack1Point = FSM.enemyController.MeleeAttack1Point.transform;
        slashEffectPrefab = FSM.enemyController.slashEffectPrefab;

        //���� �ʱ�ȭ

        //���� ����
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
        //���� �ݶ��̴� ��Ȱ��ȭ
        DisableAttackHitBox();
    }

    private IEnumerator AttackAndWaitBeforeStart(float attackTime)
    {
        //�ִϸ��̼� ���, EnableAttackHitBox() �ִϸ��̼� �̺�Ʈ�� ����
        animator.SetTrigger("isAttack");

        //���� ���� �� �̵� ���ߵ���
        rigid.velocity = new Vector2(0, rigid.velocity.y);

        //���� �ð� ��
        yield return new WaitForSeconds(attackTime);

        //���� �ݶ��̴� ��Ȱ��ȭ 
        DisableAttackHitBox();  //�ִϸ��̼� �̺�Ʈ�� ȣ�� �ȵ� �� ���

        yield return new WaitForSeconds(attackWaitTime);

        //���� ���·� ��ȯ�ǵ���
        EnemyState nextState = FSM.DecideNextState();
        FSM.ChangeState(nextState);
    }

    //�ִϸ��̼ǿ��� �ִϸ��̼� �̺�Ʈ�� ȣ���
    public void EnableAttackHitBox()
    {
        //���� ����Ʈ ����
        Instantiate(slashEffectPrefab, MeleeAttack1Point.position, Quaternion.identity, MeleeAttack1Point);
        //���� �ݶ��̴� Ȱ��ȭ
        MeleeAttack1Point.GetComponent<BoxCollider2D>().enabled = true;
    }
    //�ִϸ��̼ǿ��� �ִϸ��̼� �̺�Ʈ�� ȣ���
    public void DisableAttackHitBox()
    {
        //���� �ݶ��̴� ��Ȱ��ȭ
        MeleeAttack1Point.GetComponent<BoxCollider2D>().enabled = false;
    }
}
