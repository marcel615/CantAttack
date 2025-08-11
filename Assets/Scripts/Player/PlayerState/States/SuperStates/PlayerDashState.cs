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
        StartDash();
    }
    public override void UpdateState()
    {
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
        dashTimer = 0;
        FSM.playerController.isDashing = false;

        rigid.gravityScale = prevGravity;
    }
    public override void SetChangeState()
    {
        allowedTransitions.Add(FSM.groundState);
        allowedTransitions.Add(FSM.inAirState);
        allowedTransitions.Add(FSM.portalState);
    }
    public override bool CanChangeState(PlayerState newState)
    {
        if (newState == FSM.portalState)
            return true;
        return (isCanChange && base.CanChangeState(newState));
    }
    void StartDash()
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
