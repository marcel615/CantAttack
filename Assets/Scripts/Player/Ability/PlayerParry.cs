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
    //�и� ����Ʈ ������
    public GameObject parryEffect;

    //�⺻ ������
    float prevGravity;

    //Parry ���� �Է� ����
    bool P;  //Parry ������ �� �÷���

    //Parry ���� ������
    float ParryTime = 0.2f;
    float ParryTimer;
    bool isParryCoolTime;    //�и� ��Ÿ��
    float ParryCoolTime = 1f;
    float ParryCoolTimer;
    float ParrySuccessInvincibleTime = 0.4f;

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
        player.InvincibleTimer = ParrySuccessInvincibleTime;
        player.isInvincible = true;

        //���߿��� ��� ���Ƚ�� �ʱ�ȭ ����
        player.isParriedInAir = false;
        player.isDashedInAir = false;
        player.jumpCount = 1;
    }

    private void Update()
    {
        //�÷��̾� �и���
        if (P && !isParryCoolTime && player.canControl && !player.isParriedInAir)
        {
            ParryCoolTimer = ParryCoolTime;
            isParryCoolTime = true;

            ParryTimer = ParryTime;
            player.isParrying = true;
            player.isParriedInAir = true;

            prevGravity = rigid.gravityScale;
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(0, 0);
            playerParryCollider.enabled = true;

            //�и� ����Ʈ ����
            var Effect = Instantiate(parryEffect, transform.position, Quaternion.identity).GetComponent<ParryCircleEffect>();
            Effect.SetDeleteTime(ParryTime);
        }
        P = false;

        //���� ������ �����и� �÷��� �ʱ�ȭ
        if (player.isGrounded)
        {
            player.isParriedInAir = false;
        }
    }

    private void FixedUpdate()
    {
        if (player.isParrying)
        {
            if (ParryTimer > 0)
            {
                rigid.velocity = new Vector2(0, 0);

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
