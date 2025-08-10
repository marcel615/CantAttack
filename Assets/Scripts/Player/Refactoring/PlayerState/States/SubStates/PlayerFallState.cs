using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerState
{
    //³» ÄÄÆ÷³ÍÆ®
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        animator.SetBool("isFalling", true);
    }
    public override void UpdateState()
    {
        if (FSM.playerController.isFalling)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }
    }
    public override void FixedUpdateState()
    {
    }
    public override void Exit()
    {
        animator.SetBool("isFalling", false);
    }
    public override void SetChangeState()
    {
        //allowedTransitions.Add(FSM.groundState);
    }

}
