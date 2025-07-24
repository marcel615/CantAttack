using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;
    Player player;

    //Jump 관련 입력 변수
    bool J;
    bool jumpPressed;   //Jump 눌렀을 때 플래그
    bool J_Hold;

    //Jump 관련 변수
    float MaxJumpTime = 0.3f;
    float MaxJumpTimer;
    int jumpCount = 0;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {        
        //점프 눌렸을 때 플래그 && 땅에 서있으면 && jumpCount가 0이면 점프하도록
        if (jumpPressed && player.isGrounded && jumpCount == 0 && player.canControl)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, player.normaljumpPower);
            jumpCount = 1;
            MaxJumpTimer = MaxJumpTime;
            player.isJumping = true;

            animator.SetTrigger("isJump"); //애니메이션 변수 설정

        }
        jumpPressed = false;

        //1단 점프 한정으로 점프키를 누르고 있는 동안 점프 높이 높아지도록 
        if (J_Hold && player.isJumping)
        {
            if (jumpCount == 1) //1단 점프 한정
            {
                if (MaxJumpTimer > 0) //점프 높이 제약 걸기
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, player.normaljumpPower);
                    MaxJumpTimer -= Time.fixedDeltaTime;

                }
                else
                {
                    MaxJumpTimer = 0;
                    player.isJumping = false;
                }
            }
        }
        else
        {
            MaxJumpTimer = 0;
            player.isJumping = false;
        }

        // 점프 후 땅에 도달하면 다시 jumpCount 초기화, 애니메이션 변수 설정
        if (!player.isJumping && jumpCount != 0 && player.isGrounded)
        {
            jumpCount = 0;
        }

    }

    public void Jump(bool j)
    {
        if (j)
        {
            jumpPressed = true;
        }
        J = j;

    }
    public void Jump_Hold(bool j_Hold)
    {
        J_Hold = j_Hold;
    }
}
