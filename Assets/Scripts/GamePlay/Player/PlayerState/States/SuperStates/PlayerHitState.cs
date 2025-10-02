using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    //Hit 관련 Controller 변수
    int hittedDamage;
    Vector2 hittedPos;
    float knockbackTime;
    float knockbackPower;

    //Hit 관련 변수
    float knockbackTimer;
    bool isCanChange;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Enter()
    {
        hittedDamage = FSM.playerController.hittedDamage;
        hittedPos = FSM.playerController.hittedPos;
        knockbackTime = FSM.playerController.knockbackTime;
        knockbackPower = FSM.playerController.knockbackPower;

        isCanChange = false;
        DamageApply(hittedDamage);

    }
    public override void UpdateState()
    {

    }
    public override void FixedUpdateState()
    {
        //넉백 시간 제어하기
        if (FSM.playerController.isKnockbacked)
        {
            if (knockbackTimer > 0)
            {
                knockbackTimer -= Time.fixedDeltaTime;
            }
            else
            {
                knockbackTimer = 0;
                FSM.playerController.isKnockbacked = false;

                isCanChange = true;
            }
        }
    }
    public override void Exit()
    {
        knockbackTimer = 0;
        FSM.playerController.isKnockbacked = false;

        isCanChange = true;
    }
    public override void SetChangeState()
    {
        allowedTransitions.Add(FSM.groundState);
        allowedTransitions.Add(FSM.inAirState);
        allowedTransitions.Add(FSM.deadState);
        allowedTransitions.Add(FSM.portalState);
    }
    public override bool CanChangeState(PlayerState newState)
    {
        if (newState == FSM.deadState || newState == FSM.portalState)
            return true;
        else
            return (isCanChange && base.CanChangeState(newState));
    }

    //데미지 적용시키는 메소드
    void DamageApply(int damage)
    {
        if (FSM.playerController.CurrentHP - damage > 0)
        {
            //SFX 재생
            AudioEvents.InvokeSFXRequest(SFXType.Player_Hit, transform);

            FSM.playerController.CurrentHP -= damage;
            Knockback();
            SetInvincibleTimer();
            FSM.playerController.isKnockbackInvincible = true;
            spriteRenderer.color = new Color(1, 1, 1, 0.4f); //피격 시 반투명하게 됨
            PlayerEvents.InvokePlayerDamaged(FSM.playerController.MaxHP, FSM.playerController.CurrentHP);
        }
        else
        {
            FSM.playerController.CurrentHP = 0;
            Knockback();
            PlayerEvents.InvokePlayerDamaged(FSM.playerController.MaxHP, FSM.playerController.CurrentHP);
            PlayerEvents.InvokePlayerDead();
        }
    }
    //넉백
    void Knockback()
    {
        FSM.playerController.isKnockbacked = true;
        knockbackTimer = knockbackTime;
        //넉백 구현
        if (transform.position.x < hittedPos.x)
        {
            rigid.AddForce(new Vector2(-0.5f, 1f) * knockbackPower, ForceMode2D.Impulse);
        }
        else
        {
            rigid.AddForce(new Vector2(0.5f, 1f) * knockbackPower, ForceMode2D.Impulse);
        }
    }
    //무적 타이머 설정
    void SetInvincibleTimer()
    {
        FSM.playerController.isInvincible = true;
        FSM.playerController.InvincibleTimer = FSM.playerController.InvincibleTime_Hitted;
    }

}
