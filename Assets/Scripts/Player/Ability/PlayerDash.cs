using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
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



    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }
    private void Update()
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
    }

    public void Dash(bool d)
    {
        D = d;
    }

}
