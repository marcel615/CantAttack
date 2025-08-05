using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAChaseState : EnemyState
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    //Chase ���� Controller ����
    GameObject player;
    float chaseSpeed;

    //Chase ���� ����
    int chaseDir;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void Enter()
    {
        //Controller ���� �ʱ�ȭ
        player = FSM.enemyController.player;
        chaseSpeed = FSM.enemyController.chaseSpeed;
    }

    public override void UpdateState()
    {
        //chase ���� ���� �� chase �������� chaseSpeed�� �����̱�
        chaseDir = (player.transform.position.x > transform.position.x) ? 1 : -1;
        rigid.velocity = new Vector2(chaseDir * chaseSpeed, rigid.velocity.y);

        spriteRenderer.flipX = chaseDir < 0;
        animator.SetBool("isMoving", true);
        FSM.enemyController.isHeadToRight = (chaseDir > 0) ? 1 : -1; //chaseDir�� ����� 1 ����, ������ -1 ����
    }

    public override void Exit()
    {
        
    }
}
