using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
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

    //Parry 관련 입력 변수
    bool P;  //Parry 눌렀을 때 플래그

    //Parry 관련 변수들
    float ParryTime = 0.4f;
    float ParryTimer;
    bool isParryCoolTime;    //패리 쿨타임
    float ParryCoolTime = 1f;
    float ParryCoolTimer;



    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }
    private void FixedUpdate()
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


        /*
        //플레이어 패링기
        if (P && !isParryCoolTime && player.canControl)
        {
            ParryCoolTimer = ParryCoolTime;
            isParryCoolTime = true;

            ParryTimer = ParryTime;
            //player.isDashing = true;
            //player.InvincibleTimer = DashTime;
            //player.isInvincible = true;

            prevGravity = rigid.gravityScale;
            rigid.gravityScale = 0;
            //rigid.velocity = new Vector2(player.isHeadToRight * DashSpeed, 0);
        }
        P = false;

        if (player.isDashing)
        {
            if (ParryTimer > 0)
            {
                //rigid.velocity = new Vector2(player.isHeadToRight * DashSpeed, 0);
                ParryTimer -= Time.fixedDeltaTime;
            }
            else
            {
                ParryTimer = 0;
                //player.isDashing = false;

                rigid.gravityScale = prevGravity;
            }
        }
        //패링기 쿨타임 계산
        if (isParryCoolTime)
        {
            if (ParryCoolTimer > 0)
            {
                ParryCoolTimer -= Time.fixedDeltaTime;
            }
            else
            {
                ParryCoolTimer = 0;
                isParryCoolTime = false;
            }
        }
        */


    }
    
    public void Dash(bool d)
    {
        D = d;
    }
    public void Parry(bool p)
    {
        P = p;
    }
}
