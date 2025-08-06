using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageHandler : MonoBehaviour
{
    //내 컴포넌트
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Player player;

    //OnDamaged 관련 변수
    bool getDamage;   //OnDamaged 실행되었을 때 플래그
    Vector2 hittedPos; 
    int hittedDamage;

    //넉백 관련 변수
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
    private void FixedUpdate()
    {
        //OnDamaged 실행되면
        if (getDamage)
        {
            if (!player.isInvincible)
            {
                //status에 데미지 적용 후 이벤트 발행
                DamageApply(hittedDamage);
                PlayerEvents.InvokePlayerDamaged(player.status.MaxHP, player.status.CurrentHP);

                //무적 타이머, 넉백 타이머 실행
                player.isInvincible = true;
                player.InvincibleTimer = player.InvincibleTime_Hitted;

                player.isKnockbackInvincible = true;
                player.isKnockbacked = true;
                KnockbackTimer = KnockbackTime;

                //넉백 구현
                if (transform.position.x < hittedPos.x)
                {
                    rigid.AddForce(new Vector2(-0.5f, 1f) * KnockbackPower, ForceMode2D.Impulse);
                }
                else
                {
                    rigid.AddForce(new Vector2(0.5f, 1f) * KnockbackPower, ForceMode2D.Impulse);

                }
                spriteRenderer.color = new Color(1, 1, 1, 0.4f); //피격 시 반투명하게 됨
            }
        }
        getDamage = false;

        //넉백 시간 제어하기
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

    //이벤트 구독
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
    //피격 이벤트 발생 시
    void OnDamaged(Vector2 hitTargetPos, int damage)
    {
        getDamage = true;
        hittedPos = hitTargetPos;
        hittedDamage = damage;

    }
    void OnKnockedBackInvincibleOver()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1f); //투명해졌던거 다시 원상복귀 
    }
    //데미지 적용시키는 메소드
    void DamageApply(int damage)
    {
        if (player.status.CurrentHP - damage > 0)
        {
            player.status.CurrentHP -= damage;
        }
        else
        {
            Die();
        }
    }

    //죽는 메소드(아직 미구현)
    void Die()
    {

    }

}
