using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    Animator animator;
    Player player;

    //�⺻ ������
    float prevGravity;

    //Dash ���� �Է� ����
    bool D;  //Dash ������ �� �÷���

    //Dash ���� ������
    float DashTime = 0.4f;
    float DashTimer;
    bool isDashCoolTime;    //ȸ�� ��Ÿ��
    float DashCoolTime = 1f;
    float DashCoolTimer;
    float DashSpeed = 12f;

    //Parry ���� �Է� ����
    bool P;  //Parry ������ �� �÷���

    //Parry ���� ������
    float ParryTime = 0.4f;
    float ParryTimer;
    bool isParryCoolTime;    //�и� ��Ÿ��
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
        //�÷��̾� ȸ�Ǳ�
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
        //ȸ�Ǳ� ��Ÿ�� ���
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
        //�÷��̾� �и���
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
        //�и��� ��Ÿ�� ���
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
