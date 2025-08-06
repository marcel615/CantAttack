using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAChaseState : EnemyState
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    Animator animator;

    //Chase ���� Controller ����
    GameObject player;
    float chaseSpeed;

    //Chase ���� ����
    int chaseDir;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        //Controller ���� �ʱ�ȭ
        player = FSM.enemyController.player;
        chaseSpeed = FSM.enemyController.chaseSpeed;
    }

    public override void UpdateState()
    {
        //�˹鵿���� ���� ���ϵ���
        if (FSM.enemyController.isKnockbacked) return;
        //���Ͻð������� ���� ���ϵ���
        if (FSM.enemyController.isParryStun) return;

        //chase ���� ����
        chaseDir = (player.transform.position.x > transform.position.x) ? 1 : -1;

        //chaseDir ���� ĳ���� �¿� ����
        transform.localScale = new Vector3(chaseDir, 1, 1);
        FSM.enemyController.isHeadToRight = (chaseDir > 0) ? 1 : -1; //chaseDir�� ����� 1 ����, ������ -1 ����

        //�տ� ���� ���ų� �տ� ���� ������ chase �����̱� ������ ���ߵ���
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
