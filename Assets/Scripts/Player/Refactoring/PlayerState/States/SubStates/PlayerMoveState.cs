using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMoveState : PlayerState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    //move 관련 Controller 변수
    float normalSpeed;

    //move 관련 변수

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        normalSpeed = FSM.playerController.normalSpeed;
    }
    public override void UpdateState()
    {
    }
    public override void FixedUpdateState()
    {
        rigid.velocity = new Vector2(FSM.playerController.H * normalSpeed, rigid.velocity.y);

        if (FSM.playerController.H != 0)
        {
            //H에 따라 캐릭터 좌우 반전
            transform.localScale = new Vector3(FSM.playerController.H, 1, 1);
            FSM.playerController.isHeadToRight = (FSM.playerController.H > 0) ? 1 : -1; //H가 양수면 1 저장, 음수면 -1 저장
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        
    }
    public override void Exit()
    {
        animator.SetBool("isMoving", false);
    }
    public override void SetChangeState()
    {
        //allowedTransitions.Add(FSM.groundState);
    }
}
