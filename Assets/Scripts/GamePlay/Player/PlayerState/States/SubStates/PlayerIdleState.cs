using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;

    //idle 관련 Controller 변수

    //idle 관련 변수

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public override void Enter()
    {
    }
    public override void UpdateState()
    {
    }
    public override void FixedUpdateState()
    {
        if (FSM.playerController.H == 0)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            animator.SetBool("isMoving", false);
        }
        //rigid.velocity = new Vector2(0, rigid.velocity.y);
        //animator.SetBool("isMoving", false);
    }
    public override void Exit()
    {
    }
    public override void SetChangeState()
    {
        //allowedTransitions.Add(FSM.groundState);
    }
}
