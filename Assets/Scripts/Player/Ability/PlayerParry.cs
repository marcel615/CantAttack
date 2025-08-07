using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;
    Player player;

    //내 자식 오브젝트 관련
    public CircleCollider2D playerParryCollider;
    //패리 이펙트 프리팹
    public GameObject parryEffect;

    //기본 변수들
    float prevGravity;

    //Parry 관련 입력 변수
    bool P;  //Parry 눌렀을 때 플래그

    //Parry 관련 변수들
    float ParryTime = 0.2f;
    float ParryTimer;
    bool isParryCoolTime;    //패리 쿨타임
    float ParryCoolTime = 1f;
    float ParryCoolTimer;
    float ParrySuccessInvincibleTime = 0.4f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();

        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        //Parry의 Collider 연결
        if (playerParryCollider == null) playerParryCollider = transform.Find("Parry")?.GetComponent<CircleCollider2D>();

    }
    //이벤트 구독
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

        //공중에서 기술 사용횟수 초기화 보상
        player.isParriedInAir = false;
        player.isDashedInAir = false;
        player.jumpCount = 1;
    }

    private void Update()
    {
        //플레이어 패링기
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

            //패리 이펙트 시작
            var Effect = Instantiate(parryEffect, transform.position, Quaternion.identity).GetComponent<ParryCircleEffect>();
            Effect.SetDeleteTime(ParryTime);
        }
        P = false;

        //땅에 닿으면 공중패링 플래그 초기화
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
        //패링기 쿨타임 계산
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
