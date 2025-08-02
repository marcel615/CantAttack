using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Player player;

    //Move ���� �Է� ����
    float H;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        //�÷��̾� �⺻ ������ ����
        if (player.canControl)
        {
            rigid.velocity = new Vector2(H * player.normalSpeed, rigid.velocity.y);
        }
        //H���� ���� �÷��̾� ĳ���Ͱ� �ٶ󺸴� ���� �� �ִϸ��̼� ����
        if (H != 0 && player.canControl)
        {
            spriteRenderer.flipX = H < 0;
            animator.SetBool("isMoving", true);
            player.isHeadToRight = (H > 0) ? 1 : -1; //H�� ����� 1 ����, ������ -1 ����
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        //Move�� ���̻� ���� �ʴµ��� ��� �����̴� ��Ȳ �ذ��ϱ� ���� H = 0���� �ʱ�ȭ�����ֱ�
        H = 0;

    }

    public void Move(float h)
    {
        H = h;
    }

}
