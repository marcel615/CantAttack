using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageHandler : MonoBehaviour
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Player player;

    //OnDamaged ���� ����
    bool isKnockback;   //OnDamaged ����Ǿ��� �� �÷���
    Vector2 hittedPos; 
    int hittedDamage;

    //�˹� ���� ����
    float KnockbackTime = 0.3f;
    float KnockbackTimer;
    float KnockbackPower = 15f;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();

    }
    private void Update()
    {
        if (isKnockback)
        {
            if (!player.isInvincible)
            {
                //status�� ������ ���� �� �̺�Ʈ ����
                DamageApply(hittedDamage);
                PlayerEvents.InvokePlayerDamaged(player.status.MaxHP, player.status.CurrentHP);

                if (player.status.CurrentHP <= 0)
                {
                    player.isKnockbacked = true;
                    KnockbackTimer = KnockbackTime;
                    //�˹� ����
                    if (transform.position.x < hittedPos.x)
                    {
                        rigid.AddForce(new Vector2(-0.5f, 1f) * KnockbackPower, ForceMode2D.Impulse);
                    }
                    else
                    {
                        rigid.AddForce(new Vector2(0.5f, 1f) * KnockbackPower, ForceMode2D.Impulse);

                    }
                    isKnockback = false;
                    return;
                }

                //���� Ÿ�̸�, �˹� Ÿ�̸� ����
                player.isInvincible = true;
                player.InvincibleTimer = player.InvincibleTime_Hitted;

                player.isKnockbackInvincible = true;
                player.isKnockbacked = true;
                KnockbackTimer = KnockbackTime;

                //�˹� ����
                if (transform.position.x < hittedPos.x)
                {
                    rigid.AddForce(new Vector2(-0.5f, 1f) * KnockbackPower, ForceMode2D.Impulse);
                }
                else
                {
                    rigid.AddForce(new Vector2(0.5f, 1f) * KnockbackPower, ForceMode2D.Impulse);

                }
                spriteRenderer.color = new Color(1, 1, 1, 0.4f); //�ǰ� �� �������ϰ� ��

            }
        }
        isKnockback = false;
    }
    private void FixedUpdate()
    {
        //�˹� �ð� �����ϱ�
        if (player.isKnockbacked)
        {
            if (KnockbackTimer > 0)
            {
                KnockbackTimer -= Time.fixedDeltaTime;
            }
            else
            {
                KnockbackTimer = 0;
                player.isKnockbacked = false;
            }
        }
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        PlayerEvents.OnPlayerHitBoxHitted_PlayerDamageHandler += OnDamaged;
        PlayerEvents.OnPlayerKnockedBackInvincibleOver += OnKnockedBackInvincibleOver;
    }
    private void OnDisable()
    {
        PlayerEvents.OnPlayerHitBoxHitted_PlayerDamageHandler -= OnDamaged;
        PlayerEvents.OnPlayerKnockedBackInvincibleOver -= OnKnockedBackInvincibleOver;
    }
    //�ǰ� �̺�Ʈ �߻� ��
    void OnDamaged(Vector2 hitTargetPos, int damage)
    {
        isKnockback = true;
        hittedPos = hitTargetPos;
        hittedDamage = damage;

        /*
        if (!player.isInvincible)
        {
            ////status�� ������ ���� �� �̺�Ʈ ����
            DamageApply(hittedDamage);
            PlayerEvents.InvokePlayerDamaged(player.status.MaxHP, player.status.CurrentHP);
        }
        */
    }
    void OnKnockedBackInvincibleOver()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1f); //������������ �ٽ� ���󺹱� 
    }
    //������ �����Ű�� �޼ҵ�
    void DamageApply(int damage)
    {
        if (player.status.CurrentHP - damage > 0)
        {
            player.status.CurrentHP -= damage;
            //isKnockback = true;
        }
        else
        {
            player.status.CurrentHP = 0;
            PlayerEvents.InvokePlayerDead();
        }
    }


}
