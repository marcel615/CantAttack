using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;

    //Dash 관련 Controller 변수
    float dashCoolTime;
    float dashSpeed;
    float dashTime;

    //Dash 관련 변수
    float dashTimer;
    float prevGravity;
    bool isCanChange;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        dashCoolTime = FSM.playerController.dashCoolTime;
        dashSpeed = FSM.playerController.dashSpeed;
        dashTime = FSM.playerController.dashTime;

        isCanChange = false;
    }
    public override void UpdateState()
    {
        //플레이어 회피기
        if (!FSM.playerController.isDashCoolTime && !FSM.playerController.isDashedInAir)
        {
            FSM.playerController.dashCoolTimer = dashCoolTime;
            FSM.playerController.isDashCoolTime = true;

            dashTimer = dashTime;
            FSM.playerController.isDashing = true;
            FSM.playerController.isDashedInAir = true;

            FSM.playerController.InvincibleTimer = dashTime;
            FSM.playerController.isInvincible = true;

            prevGravity = rigid.gravityScale;
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(FSM.playerController.isHeadToRight * dashSpeed, 0);
            animator.SetTrigger("isDash");
        }
    }
    public override void FixedUpdateState()
    {
        if (FSM.playerController.isDashing)
        {
            if (dashTimer > 0)
            {
                rigid.velocity = new Vector2(FSM.playerController.isHeadToRight * dashSpeed, 0);
                dashTimer -= Time.fixedDeltaTime;
            }
            else
            {
                dashTimer = 0;
                FSM.playerController.isDashing = false;

                rigid.gravityScale = prevGravity;

                isCanChange = true;
            }
        }
    }
    public override void Exit()
    {
    }
    public override void SetChangeState()
    {
        allowedTransitions.Add(FSM.groundState);
        allowedTransitions.Add(FSM.inAirState);
        allowedTransitions.Add(FSM.portalState);
    }
    public override bool CanChangeState(PlayerState newState)
    {
        return (isCanChange && base.CanChangeState(newState));
    }
    

}
