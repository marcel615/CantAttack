using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAAttackState : EnemyState
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    Animator animator;

    //�� �ڽ� ������Ʈ
    public Transform MeleeAttack1Point;

    //���� ���� Controller ����
    public float attackTime;
    public float attackWaitTime;

    //���� ���� ����
    public GameObject slashEffectPrefab;
    bool isAttacking;
    bool isWaiting;



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

        //���� �ʱ�ȭ
    }
    public override void UpdateState()
    {
        //�����ϴ� �ð����� �ٷ� ����
        if (isAttacking) return;
        //��ٸ��� �ð����� �ٷ� ����
        if (isWaiting) return;
        //���� Ʈ���ſ��� ����� ChaseState�� ��ȯ �� ����
        if (!FSM.enemyController.isAttackEnable)
        {
            FSM.ChangeState(FSM.chaseState);
            return;
        }
        
        //���� ����
        StartCoroutine(AttackAndWaitBeforeStart(attackTime));

    }
    public override void Exit()
    {
    }
    private IEnumerator AttackAndWaitBeforeStart(float attackTime)
    {
        //���� ���� �÷���
        isAttacking = true;

        //�ִϸ��̼� ���, EnableAttackHitBox() �ִϸ��̼� �̺�Ʈ�� ����
        animator.SetTrigger("isAttack");

        //���� ���� �� �̵� ���ߵ���
        rigid.velocity = new Vector2(0, rigid.velocity.y);

        //���� �ð� ��
        yield return new WaitForSeconds(attackTime);

        //���� ���� �÷���
        isAttacking = false;

        //���� �ݶ��̴� ��Ȱ��ȭ 
        DisableAttackHitBox();  //�ִϸ��̼� �̺�Ʈ�� ȣ�� �ȵ� �� ���

        //Wait ���� �÷���
        isWaiting = true;

        yield return new WaitForSeconds(attackWaitTime);

        //Wait ���� �÷���
        isWaiting = false;
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
