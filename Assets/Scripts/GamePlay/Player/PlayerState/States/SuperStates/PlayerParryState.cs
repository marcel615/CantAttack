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
    ShieldDataSO currentShield;
    ParryModeDataSO currentParryMode;


    //Parry 관련 변수
    float parryTimer;
    float prevGravity;
    bool isParrySuccess;
    Vector2 parryActionDir;
    bool isDirSet;
    GameObject parryActionTarget;
    bool isTargetSet;
    bool isParryAction;
    float parryActionTimer;
    Vector2 impactDirection;

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
        currentShield = FSM.playerController.currentShield;
        currentParryMode = FSM.playerController.currentParryMode;

        parryActionDir = Vector2.zero;
        isDirSet = false;
        parryActionTarget = null;
        isTargetSet = false;
        isParrySuccess = false;
        isParryAction = false;
        impactDirection = Vector2.zero;
        isCanChange = false;
        StartParry();
    }
    public override void UpdateState()
    {
    }
    public override void FixedUpdateState()
    {
        //패리 성공 시 로직
        if (isParrySuccess)
        {
            //방패 분기
            switch (currentShield.shieldType)
            {
                case ShieldType.Impact:
                    if (!isDirSet && !isTargetSet) break;

                    if (!isParryAction)
                    {
                        if (isDirSet)
                        {
                            impactDirection = -parryActionDir;
                        }
                        else if (isTargetSet)
                        {
                            impactDirection = -(parryActionTarget.transform.position - transform.position).normalized;
                        }
                        rigid.AddForce(impactDirection * 25f, ForceMode2D.Impulse);

                        isParryAction = true;
                        parryActionTimer = 0.3f;
                    }
                    else
                    {
                        if (parryActionTimer > 0)
                        {
                            parryActionTimer -= Time.fixedDeltaTime;
                        }
                        else
                        {
                            parryActionTimer = 0;
                            isDirSet = false;
                            isTargetSet = false;

                            isCanChange = true;

                            //플랫폼의 아주아주 끝에서 패리를 할 경우 다음 상태로 전환하지 못하게 되는 버그 임시 조치
                            FSM.ChangeState(FSM.groundState);
                        }
                    }
                    break;

                default:
                    isCanChange = true;

                    //플랫폼의 아주아주 끝에서 패리를 할 경우 다음 상태로 전환하지 못하게 되는 버그 임시 조치
                    FSM.ChangeState(FSM.groundState);

                    break;
            }
            //패리 모드 분기
            switch (currentParryMode.parryModeType)
            {
                case ParryModeType.Absorb:
                    isCanChange = true;

                    //플랫폼의 아주아주 끝에서 패리를 할 경우 다음 상태로 전환하지 못하게 되는 버그 임시 조치
                    FSM.ChangeState(FSM.groundState);

                    break;

                case ParryModeType.Empty:
                    isCanChange = true;

                    //플랫폼의 아주아주 끝에서 패리를 할 경우 다음 상태로 전환하지 못하게 되는 버그 임시 조치
                    FSM.ChangeState(FSM.groundState);

                    break;

                default:
                    break;
            }
        }
        else //패리 진행 중 로직
        {
            //패리 진행
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
        //PlayerParry가 투사체 패링에 성공했을 때
        PlayerEvents.OnProjectileParried += ProjectileParrySuccess;
        //parryAction의 방향이 정해졌을 때
        PlayerEvents.OnParryActionDirectionSet += DirectionSet;
        //parryAction의 타겟이 정해졌을 때
        PlayerEvents.OnParryActionTargetSet += TargetSet;
    }
    private void OnDisable()
    {
        //PlayerParry가 투사체 패링에 성공했을 때
        PlayerEvents.OnProjectileParried -= ProjectileParrySuccess;
        //parryAction의 방향이 정해졌을 때
        PlayerEvents.OnParryActionDirectionSet -= DirectionSet;
        //parryAction의 타겟이 정해졌을 때
        PlayerEvents.OnParryActionTargetSet -= TargetSet;
    }
    void ProjectileParrySuccess(ProjectileBase prefab, GameObject sender)
    {
        isParrySuccess = true;

        playerParryCollider.enabled = false;
        parryTimer = 0;
        FSM.playerController.isParrying = false;
        animator.SetBool("isParry", false);
        rigid.gravityScale = prevGravity;
    }
    void DirectionSet(Vector2 dir)
    {
        isDirSet = true;
        parryActionDir = dir;
    }
    void TargetSet(GameObject gameObject)
    {
        isTargetSet = true;
        parryActionTarget = gameObject;
    }
}
