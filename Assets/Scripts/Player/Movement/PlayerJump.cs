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
    int jumpCount = 0;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }
    private void Update()
    {
        //���� ������ �� �÷��� && ���� �������� && jumpCount�� 0�̸� �����ϵ���
        if (J && player.isGrounded && jumpCount == 0 && player.canControl)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, player.normalJumpPower);
            jumpCount = 1;
            MaxJumpTimer = MaxJumpTime;
            player.isJumping = true;

            animator.SetTrigger("isJump"); //�ִϸ��̼� ���� ����

        }
        //�������� ����
        if ((J && jumpCount == 1 && !player.isGrounded && player.canControl) || (J && jumpCount == 0 && player.isFalling && player.canControl))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, player.doubleJumpPower);
            jumpCount = 2;
            player.isJumping = true;

            animator.SetTrigger("isDoubleJump"); //�ִϸ��̼� ���� ����

            //�ִϸ����� ����
        }
        J = false;
    }

    private void FixedUpdate()
    {        

        //1�� ���� �������� ����Ű�� ������ �ִ� ���� ���� ���� ���������� 
        if (J_Hold && player.isJumping)
        {
            if (jumpCount == 1) //1�� ���� ����
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

        // ���� �� ���� �����ϸ� �ٽ� jumpCount �ʱ�ȭ, �ִϸ��̼� ���� ����
        if (!player.isJumping && jumpCount != 0 && player.isGrounded)
        {
            jumpCount = 0;
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
