using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;

    //jump 관련 Controller 변수
    float normalJumpPower;
    float doubleJumpPower;
    float MaxJumpTime;

    //jump 관련 변수
    float MaxJumpTimer;
    bool canJumpHold;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        normalJumpPower = FSM.playerController.normalJumpPower;
        doubleJumpPower = FSM.playerController.doubleJumpPower;
        MaxJumpTime = FSM.playerController.MaxJumpTime;
    }
    public override void UpdateState()
    {
        if (FSM.playerController.isJumpEvent && FSM.playerController.isGrounded && FSM.playerController.jumpCount == 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, normalJumpPower);
            FSM.playerController.jumpCount = 1;
            MaxJumpTimer = MaxJumpTime;
            FSM.playerController.isJumping = true;

            //점프홀드 가능하도록 플래그 설정
            canJumpHold = true;

            animator.SetTrigger("isJump"); //애니메이션 변수 설정

            //SFX 재생
            AudioEvents.InvokeSFXRequest(SFXType.Player_Jump, transform);
        }
        //더블점프 구현
        if ((FSM.playerController.isJumpEvent && FSM.playerController.jumpCount == 1 && !FSM.playerController.isGrounded) || (FSM.playerController.isJumpEvent && FSM.playerController.jumpCount == 0 && FSM.playerController.isFalling))
        {
            if(!FSM.playerController.isDoubleJumpUnlocked) return;

            rigid.velocity = new Vector2(rigid.velocity.x, doubleJumpPower);
            FSM.playerController.jumpCount = 2;
            FSM.playerController.isJumping = true;

            animator.SetTrigger("isDoubleJump"); //애니메이션 변수 설정

            //SFX 재생
            AudioEvents.InvokeSFXRequest(SFXType.Player_Jump, transform);
        }

        // 점프 후 땅에 도달하면 다시 jumpCount 초기화, 애니메이션 변수 설정
        if (!FSM.playerController.isJumping && FSM.playerController.jumpCount != 0 && FSM.playerController.isGrounded)
        {
            FSM.playerController.jumpCount = 0;
        }

        //점프 한 번 소비하고 플래그 초기화
        FSM.playerController.isJumpEvent = false;
    }
    public override void FixedUpdateState()
    {
        //1단 점프 한정으로 점프키를 누르고 있는 동안 점프 높이 높아지도록 
        if (FSM.playerController.isJumpHoldEvent && FSM.playerController.isJumping && canJumpHold)
        {
            if (FSM.playerController.jumpCount == 1) //1단 점프 한정
            {
                if (MaxJumpTimer > 0) //점프 높이 제약 걸기
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, normalJumpPower);
                    MaxJumpTimer -= Time.fixedDeltaTime;
                }
                else
                {
                    MaxJumpTimer = 0;
                    FSM.playerController.isJumping = false;
                }
            }
        }
        else
        {
            MaxJumpTimer = 0;
            FSM.playerController.isJumping = false;
        }

        //점프홀드 한 번 소비하고 플래그 초기화
        FSM.playerController.isJumpHoldEvent = false;
    }
    public override void Exit()
    {
        //점프홀드 중에 다른 행동으로 점프홀드가 취소될 경우 플래그 초기화
        canJumpHold = false;
    }
    public override void SetChangeState()
    {
        //allowedTransitions.Add(FSM.groundState);
    }

}
