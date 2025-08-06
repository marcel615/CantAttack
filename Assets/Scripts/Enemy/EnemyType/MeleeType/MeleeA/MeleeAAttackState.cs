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
    float attackMinWaitTime;
    float attackMaxWaitTime;
    public float attackTime;

    //���� ���� ����
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
        //Controller ���� �ʱ�ȭ
        attackMinWaitTime = FSM.enemyController.attackMinWaitTime;
        attackMaxWaitTime = FSM.enemyController.attackMaxWaitTime;
        attackTime = FSM.enemyController.attackTime;

        //���� �ʱ�ȭ
        attackWaitTime = Random.Range(attackMinWaitTime, attackMaxWaitTime);    //���� �� �� �ϰ� ��� ���� �ð� 
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

        //���� ����Ʈ ����
        Instantiate(slashEffectPrefab, MeleeAttack1Point.position, Quaternion.identity, MeleeAttack1Point);
        //���� �ݶ��̴� Ȱ��ȭ
        MeleeAttack1Point.GetComponent<BoxCollider2D>().enabled = true;
        //���� ���� �� �ӵ� ���ߵ���
        rigid.velocity = new Vector2(0, rigid.velocity.y);

        //���� �ð� ��
        yield return new WaitForSeconds(attackTime);

        //���� ���� �÷���
        isAttacking = false;

        //���� �ݶ��̴� ��Ȱ��ȭ
        MeleeAttack1Point.GetComponent<BoxCollider2D>().enabled = false;

        //Wait ���� �÷���
        isWaiting = true;
        yield return new WaitForSeconds(attackWaitTime);

        //Wait ���� �÷���
        isWaiting = false;
    }
}
