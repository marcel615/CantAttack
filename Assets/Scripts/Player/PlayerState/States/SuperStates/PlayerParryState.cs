using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : PlayerState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;

    //Parry 관련 Controller 변수
    float parryCoolTime;
    float parryTime;
    CircleCollider2D playerParryCollider;
    GameObject parryEffect;

    //Parry 관련 변수
    float parryTimer;
    float prevGravity;
    bool isCanChange;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        parryCoolTime = FSM.playerController.parryCoolTime;
        parryTime = FSM.playerController.parryTime;
        playerParryCollider = FSM.playerController.playerParryCollider;
        parryEffect = FSM.playerController.parryEffect;

        isCanChange = false;
        StartParry();
    }
    public override void UpdateState()
    {
    }
    public override void FixedUpdateState()
    {
        if (FSM.playerController.isParrying)
        {
            if (parryTimer > 0)
            {
                rigid.velocity = new Vector2(0, 0);

                parryTimer -= Time.fixedDeltaTime;
            }
            else
            {
                playerParryCollider.enabled = false;
                parryTimer = 0;
                FSM.playerController.isParrying = false;
                animator.SetBool("isParry", false);

                rigid.gravityScale = prevGravity;

                isCanChange = true;

                //플랫폼의 아주아주 끝에서 패리를 할 경우 다음 상태로 전환하지 못하게 되는 버그 임시 조치
                FSM.ChangeState(FSM.groundState);
            }
        }
    }
    public override void Exit()
    {
        playerParryCollider.enabled = false;
        parryTimer = 0;
        FSM.playerController.isParrying = false;
        animator.SetBool("isParry", false);

        rigid.gravityScale = prevGravity;
    }
    public override void SetChangeState()
    {
        allowedTransitions.Add(FSM.groundState);
        allowedTransitions.Add(FSM.inAirState);
        allowedTransitions.Add(FSM.portalState);
    }
    public override bool CanChangeState(PlayerState newState)
    {
        if (newState == FSM.portalState)
            return true;
        return (isCanChange && base.CanChangeState(newState));
    }
    void StartParry()
    {
        //SFX 재생
        AudioEvents.InvokeSFXRequest(SFXType.Player_Parry, transform);

        FSM.playerController.parryCoolTimer = parryCoolTime;
        FSM.playerController.isParryCoolTime = true;

        parryTimer = parryTime;
        FSM.playerController.isParrying = true;
        FSM.playerController.isParriedInAir = true;

        prevGravity = rigid.gravityScale;
        rigid.gravityScale = 0;
        rigid.velocity = new Vector2(0, 0);
        playerParryCollider.enabled = true;
        animator.SetBool("isParry", true);

        //패리 이펙트 시작
        var Effect = Instantiate(parryEffect, transform.position, Quaternion.identity).GetComponent<ParryCircleEffect>();
        Effect.SetDeleteTime(parryTime);
    }

    //이벤트 구독
    private void OnEnable()
    {
        //PlayerParry가 성공했을 때
        PlayerEvents.OnPlayerParrySuccess += ParrySuccess;
    }
    private void OnDisable()
    {
        //PlayerParry가 성공했을 때
        PlayerEvents.OnPlayerParrySuccess -= ParrySuccess;
    }
    void ParrySuccess()
    {
        Debug.Log("Parry Success");
        FSM.playerController.InvincibleTimer = FSM.playerController.parrySuccessInvincibleTime;
        FSM.playerController.isInvincible = true;

        //공중에서 기술 사용횟수 초기화 보상
        FSM.playerController.isParriedInAir = false;
        FSM.playerController.isDashedInAir = false;
        FSM.playerController.jumpCount = 1;
    }
}
