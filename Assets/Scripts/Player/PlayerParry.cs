using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    Animator animator;
    Player player;

    //�� �ڽ� ������Ʈ ����
    public CircleCollider2D playerParryCollider;

    //�⺻ ������
    float prevGravity;

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

        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        //Parry�� Collider ����
        if (playerParryCollider == null) playerParryCollider = transform.Find("Parry")?.GetComponent<CircleCollider2D>();

    }
    //�̺�Ʈ ����
    private void OnEnable()
    {
        PlayerEvents.OnPlayerParrySuccess += ParrySuccess;
    }
    private void OnDisable()
    {
        PlayerEvents.OnPlayerParrySuccess -= ParrySuccess;

    }
    void ParrySuccess()
    {
        Debug.Log("Parry Success");
        player.InvincibleTimer = ParryTime;
        player.isInvincible = true;
    }


    private void FixedUpdate()
    {
        //�÷��̾� �и���
        if (P && !isParryCoolTime && player.canControl)
        {
            ParryCoolTimer = ParryCoolTime;
            isParryCoolTime = true;

            ParryTimer = ParryTime;
            player.isParrying = true;

            prevGravity = rigid.gravityScale;
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(0, 0);
        }
        P = false;

        if (player.isParrying)
        {
            if (ParryTimer > 0)
            {
                rigid.velocity = new Vector2(0, 0);
                playerParryCollider.enabled = true;

                ParryTimer -= Time.fixedDeltaTime;
            }
            else
            {
                playerParryCollider.enabled = false;
                ParryTimer = 0;
                player.isParrying = false;

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
        

    }
    public void Parry(bool p)
    {
        P = p;
    }


}
