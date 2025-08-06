using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <구현기능>
    /// 
    /// 땅 체크
    /// 조작 가능 플래그 체크
    /// 무적 체크
    /// 플레이어 낙하 상황 체크
    /// 세이브 로드
    /// </구현기능>
    
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static Player Instance;

    //내 컴포넌트
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D detectCollider;
    Animator animator;
    public PlayerStatus status;

    //내 자식 오브젝트 관련
    public Transform groundCheckObj;
    public CapsuleCollider2D playerHitBoxCollider;

    //세이브, 로드 변수
    public Vector2 savePosition;

    //컨텍스트 enum 정보
    public InputContext playerContext = InputContext.Player;

    //기본 변수들
    public float normalSpeed = 6f;
    public float normalJumpPower = 10f;
    public float doubleJumpPower = 20f;
    public float isHeadToRight = 1f; //캐릭터가 바라보는 방향(1이면 오른쪽, -1이면 왼쪽)

    //조작 제한 플래그
    public bool canControl = true; //조작 가능 플래그
    public bool isJumping;          //점프 관련 플래그
    public bool isDashing;          //회피 관련 플래그
    public bool isParrying;         //패링 관련 플래그
    public bool isKnockbacked;      //피격 시 넉백 관련 플래그
    public bool isGrounded;         //바닥 체크 플래그
    public bool isPortalEnter;      //포탈 진입 플래그
    public bool isFalling;          //낙하 상태 플래그

    //발 밑에 땅이 있는지 체크 관련 변수들
    float checkRadius = 0.2f;
    LayerMask groundLayer;

    //무적 및 피격 관련 변수들
    public bool isInvincible = false;
    public float InvincibleTime_Hitted = 1.5f;
    public float InvincibleTimer;
    public bool isKnockbackInvincible;


    private void Awake()
    {
        // 기존 인스턴스가 존재할 때 && 지금 새로운 인스턴스가 생성되려고 할 때
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //중복되지 않도록 지금 새롭게 생성되는 놈은 파괴시킨다
            return;
        }
        // 인스턴스 처음 할당
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //내 컴포넌트 연결
        rigid = GetComponent<Rigidbody2D>();
        detectCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        status = GetComponent<PlayerStatus>();

        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        //HitBox의 Collider 연결
        if (playerHitBoxCollider == null) playerHitBoxCollider = transform.Find("HitBox")?.GetComponent<CapsuleCollider2D>();
        //땅 체크 오브젝트 연결
        if (groundCheckObj == null) groundCheckObj = transform.Find("GroundCheckObject")?.GetComponent<Transform>();
        
        groundLayer = LayerMask.GetMask("Ground","");

    }
    private void FixedUpdate()
    {
        //땅 위에 있는지 체크
        isGrounded = Physics2D.OverlapCircle(groundCheckObj.position, checkRadius, groundLayer);

        //사용자 조작이 가능한 상태인지 판별
        if (!isKnockbacked && !isDashing && !isParrying)
        {
            canControl = true;
        }
        else
        {
            canControl = false;
        }

        //무적 시간 제어하기
        if (isInvincible)
        {
            if (InvincibleTimer > 0)
            {
                playerHitBoxCollider.enabled = false;
                InvincibleTimer -= Time.fixedDeltaTime;
            }
            else
            {
                playerHitBoxCollider.enabled = true;
                InvincibleTimer = 0;
                isInvincible = false;
                if (isKnockbackInvincible)
                {
                    isKnockbackInvincible = false;
                    PlayerEvents.InvokePlayerKnockedBackInvincibleOver();                    
                }
            }
        }

        //플레이어 캐릭터가 떨어지고 있을 때 작업. 1.애니메이션 변수 조정
        if (rigid.velocity.y < -0.1f)
        {
            isFalling = true;
            animator.SetBool("isFalling", true);
        }
        else
        {
            isFalling = false;
            animator.SetBool("isFalling", false);
        }
    }
    private void OnEnable()
    {
        //세이브슬롯에서 게임씬으로 로드가 완료되었을 때
        MapEvents.OnSavedSceneLoaded += OnSavedSceneLoaded;
    }
    private void OnDisable()
    {
        //세이브슬롯에서 게임씬으로 로드가 완료되었을 때
        MapEvents.OnSavedSceneLoaded -= OnSavedSceneLoaded;
    }
    public void Init()
    {
        PlayerEvents.InvokePlayerInstance(this);
    }

    //세이브 로드 이후 초기화
    void OnSavedSceneLoaded()
    {
        //플레이어 위치 초기화
        transform.position = savePosition;

        //플레이어 스폰 이벤트 발행
        PlayerEvents.InvokePlayerSpawned_HPUIManager(status.MaxHP, status.CurrentHP);
    }

}
