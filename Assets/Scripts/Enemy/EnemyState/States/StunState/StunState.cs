using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : EnemyState
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    Animator animator;

    //�� �ڽ� ������Ʈ

    //���� ���� Controller ����
    float parryStunTime;

    //���� ���� ����
    Coroutine stunCoroutine;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        //Controller ���� �ʱ�ȭ
        parryStunTime = FSM.enemyController.parryStunTime;

        //���� �ʱ�ȭ

        //ParryStun �ڷ�ƾ ����
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

        //�и��� ���� ���� �÷��� false��
        FSM.enemyController.isParryStun = false;
        //�ִϸ��̼� ��� ����
        animator.SetBool("isStun", false);

    }
    private IEnumerator ParryStun()
    {
        //�и��� ���� ���� �÷��� true��
        FSM.enemyController.isParryStun = true;

        //�ִϸ��̼� ���
        animator.SetBool("isStun", true);

        //�̵� ���ߵ���
        rigid.velocity = new Vector2(0, rigid.velocity.y);

        //���� �ð� ��ٸ���
        yield return new WaitForSeconds(parryStunTime);


        //�и��� ���� ���� �÷��� false��
        FSM.enemyController.isParryStun = false;

        //�ִϸ��̼� ��� ����
        animator.SetBool("isStun", false);

        //���� ���·� ��ȯ�ǵ���
        EnemyState nextState = FSM.DecideNextState();
        FSM.ChangeState(nextState);
    }
}
