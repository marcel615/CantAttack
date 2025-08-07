using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    Animator animator;
    Player player;

    //Jump ���� �Է� ����
    bool J; //Jump ������ �� �÷���
    bool J_Hold;

    //Jump ���� ����
    float MaxJumpTime = 0.35f;
    float MaxJumpTimer;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }
    private void Update()
    {
        //���� ������ �� �÷��� && ���� �������� && jumpCount�� 0�̸� �����ϵ���
        if (J && player.isGrounded && player.jumpCount == 0 && player.canControl)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, player.normalJumpPower);
            player.jumpCount = 1;
            MaxJumpTimer = MaxJumpTime;
            player.isJumping = true;

            animator.SetTrigger("isJump"); //�ִϸ��̼� ���� ����

        }
        //�������� ����
        if ((J && player.jumpCount == 1 && !player.isGrounded && player.canControl) || (J && player.jumpCount == 0 && player.isFalling && player.canControl))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, player.doubleJumpPower);
            player.jumpCount = 2;
            player.isJumping = true;

            animator.SetTrigger("isDoubleJump"); //�ִϸ��̼� ���� ����

            //�ִϸ����� ����
        }
        J = false;

        // ���� �� ���� �����ϸ� �ٽ� jumpCount �ʱ�ȭ, �ִϸ��̼� ���� ����
        if (!player.isJumping && player.jumpCount != 0 && player.isGrounded)
        {
            player.jumpCount = 0;
        }
    }

    private void FixedUpdate()
    {
        //1�� ���� �������� ����Ű�� ������ �ִ� ���� ���� ���� ���������� 
        if (J_Hold && player.isJumping)
        {
            if (player.jumpCount == 1) //1�� ���� ����
            {
                if (MaxJumpTimer > 0) //���� ���� ���� �ɱ�
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, player.normalJumpPower);
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
    }

    public void Jump(bool j)
    {
        J = j;
    }
    public void Jump_Hold(bool j_Hold)
    {
        J_Hold = j_Hold;
    }
}
