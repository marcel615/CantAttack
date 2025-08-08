using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeADeadState : EnemyState
{
    //�� ������Ʈ
    SpriteRenderer spriteRenderer; 
    Animator animator;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public override void Enter()
    {
        //�ݶ��̴� ����
        FSM.enemyController.DeadDisableColliders();
        //Dead �� �������ϰ� ��
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //Dead �ִϸ��̼� ����
        animator.SetBool("isDead", true);
    }
    public override void UpdateState()
    {
    }
    public override void Exit()
    {
    }
}
