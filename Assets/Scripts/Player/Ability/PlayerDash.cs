using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;
    Player player;

    //기본 변수들
    float prevGravity;

    //Dash 관련 입력 변수
    bool D;  //Dash 눌렀을 때 플래그

    //Dash 관련 변수들
    float DashTime = 0.4f;
    float DashTimer;
    bool isDashCoolTime;    //회피 쿨타임
    float DashCoolTime = 1f;
    float DashCoolTimer;
    float DashSpeed = 12f;



    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }
    private void Update()
    {
        //플레이어 회피기
        if (D && !isDashCoolTime && player.canControl)
        {
            DashCoolTimer = DashCoolTime;
            isDashCoolTime = true;

            DashTimer = DashTime;
            player.isDashing = true;
            player.InvincibleTimer = DashTime;
            player.isInvincible = true;

            prevGravity = rigid.gravityScale;
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(player.isHeadToRight * DashSpeed, 0);
        }
        D = false;
    }
    private void FixedUpdate()
    {
        if (player.isDashing)
        {
            if (DashTimer > 0)
            {
                rigid.velocity = new Vector2(player.isHeadToRight * DashSpeed, 0);
                DashTimer -= Time.fixedDeltaTime;
            }
            else
            {
                DashTimer = 0;
                player.isDashing = false;

                rigid.gravityScale = prevGravity;
            }
        }
        //회피기 쿨타임 계산
        if (isDashCoolTime)
        {
            if (DashCoolTimer > 0)
            {
                DashCoolTimer -= Time.fixedDeltaTime;
            }
            else
            {
                DashCoolTimer = 0;
                isDashCoolTime = false;
            }
        }
    }

    public void Dash(bool d)
    {
        D = d;
    }

}
