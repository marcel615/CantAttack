using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static PlayerController Instance;

    //내 컴포넌트
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D detectCollider;
    Animator animator;
    public PlayerFSM FSM;

    //내 자식 오브젝트 관련
    public Transform groundCheckObj;
    public CapsuleCollider2D playerHitBoxCollider;
    public Transform cameraFollowTransform;

    //타 오브젝트 참조
    public InputManager inputManager;
    public Player player;

    //컨텍스트 enum 정보
    public InputContext thisContext = InputContext.Player;

    //세이브, 로드 변수
    public int MaxHP;
    public int CurrentHP;

    //조작 제한 플래그
    //public bool canControl = true; //조작 가능 플래그

    //전체 정보

    //기본 변수들
    public float normalSpeed = 6f;
    public float normalJumpPower = 10f;
    public float doubleJumpPower = 20f;
    public float isHeadToRight = 1f; //캐릭터가 바라보는 방향(1이면 오른쪽, -1이면 왼쪽)


    //무적 관련 변수
    public bool isInvincible;
    public float InvincibleTimer;
    public float InvincibleTime_Hitted = 1.5f;
    public bool isKnockbackInvincible;

    //Idle 관련 변수

    //땅 위인지 체크 관련 변수
    public bool isGrounded;         //바닥 체크 플래그

    //Move 관련 변수
    public bool isMoveInput;        //Horizontal 입력이 들어오고 있는지 플래그
    public float H;

    //점프 관련 변수
    public bool isJumpEvent;
    public bool isJumpHoldEvent;
    public bool isJumping;          //점프 관련 플래그
    public int jumpCount;           //점프 횟수 변수
    public float MaxJumpTime = 0.35f;

    //낙하 상태인지 체크 관련 변수
    public bool isFalling;          //낙하 상태 플래그

    //대시 관련 변수
    public bool isDashEvent;
    public bool isDashing;          //회피 관련 플래그
    public bool isDashedInAir;      //공중에서 이미 대시했는지 여부
    public float dashSpeed = 12f;
    public float dashTime = 0.4f;
    public float dashCoolTimer;
    public float dashCoolTime = 1f;
    public bool isDashCoolTime;

    //패리 관련 변수
    public bool isParryEvent;
    public bool isParrying;         //패링 관련 플래그
    public bool isParriedInAir;     //공중에서 이미 패링했는지 여부
    public float parryTime = 0.2f;
    public float parryCoolTimer;
    public float parryCoolTime = 1f;
    public bool isParryCoolTime;           //패리 쿨타임
    public float parrySuccessInvincibleTime = 0.4f;
    public CircleCollider2D playerParryCollider;    //패리 콜라이더
    public GameObject parryEffect;  //패리 이펙트 프리팹


    //피격되었을 때 관련 변수
    public bool isKnockbacked;      //피격 시 넉백 관련 플래그

    //포탈 이동 관련 변수
    public bool isPortalEnter;      //포탈 진입 플래그


    //죽음 관련 변수


    //발 밑에 땅이 있는지 체크 관련 변수들
    float checkRadius = 0.2f;
    LayerMask groundLayer;


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

        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        //HitBox의 Collider 연결
        if (playerHitBoxCollider == null) playerHitBoxCollider = transform.Find("HitBox")?.GetComponent<CapsuleCollider2D>();
        //땅 체크 오브젝트 연결
        if (groundCheckObj == null) groundCheckObj = transform.Find("GroundCheckObject")?.GetComponent<Transform>();
        //땅 체크 레이어 설정
        groundLayer = LayerMask.GetMask("Ground", "");

    }
    private void Update()
    {
        //Move 관련
        if (inputManager.currentContext == thisContext)
        {
            H = inputManager.H;
            if (H != 0)
                isMoveInput = true;
            else isMoveInput = false;
        }
        else
        {
            H = 0;
            isMoveInput = false;
        }
        

        //무적 관련
        if (isInvincible)
        {
            playerHitBoxCollider.enabled = false;
        }
    }
    private void FixedUpdate()
    {
        //땅 위에 있는지 체크
        isGrounded = Physics2D.OverlapCircle(groundCheckObj.position, checkRadius, groundLayer);
        if(isGrounded)
        {
            if (FSM.currentState.CanChangeState(FSM.groundState))
                FSM.ChangeState(FSM.groundState);

            //땅에 닿으면 공중대시 플래그 초기화 
            //나중에 이런 플래그들은 몽땅 다른 곳에서 컨트롤하자
            isDashedInAir = false;
            isParriedInAir = false;
        }

        //무적 관련
        if (InvincibleTimer > 0)
        {
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

        //낙하 상태 관련
        if (rigid.velocity.y < -0.1f)
        {
            isFalling = true;
            //animator.SetBool("isFalling", true);
        }
        else
        {
            isFalling = false;
            //animator.SetBool("isFalling", false);
        }
        if (isFalling)
        {
            if (FSM.currentState.CanChangeState(FSM.inAirState))
                FSM.ChangeState(FSM.inAirState);
        }

        //쿨타임 관련    나중에 따로 스크립트 빼자 플래그 계산들이랑
        //회피기 쿨타임
        if (isDashCoolTime)
        {
            if (dashCoolTimer > 0)
            {
                dashCoolTimer -= Time.fixedDeltaTime;
            }
            else
            {
                dashCoolTimer = 0;
                isDashCoolTime = false;
            }
        }
        //패링기 쿨타임
        if (isParryCoolTime)
        {
            if (parryCoolTimer > 0)
            {
                parryCoolTimer -= Time.fixedDeltaTime;
            }
            else
            {
                parryCoolTimer = 0;
                isParryCoolTime = false;
            }
        }
    }

    //이벤트 구독
    private void OnEnable()
    {
        //점프 이벤트 구독
        InputEvents.Player.OnJump += OnJump;
        //점프 홀딩 이벤트 구독
        InputEvents.Player.OnJumpHold += OnJumpHold;
        //대쉬 이벤트 구독
        InputEvents.Player.OnDash += OnDash;
        //패링 이벤트 구독
        InputEvents.Player.OnParry += OnParry;
        //ESC 이벤트 (시스템 메뉴 열기) 구독
        InputEvents.Player.OnCancel += OnCancel;
        //Interact 이벤트 구독
        InputEvents.Player.OnInteract += OnInteract;


        //세이브슬롯에서 게임씬으로 로드가 완료되었을 때
        MapEvents.OnSavedSceneLoaded += OnSavedSceneLoaded;
    }
    private void OnDisable()
    {
        //점프 이벤트 구독
        InputEvents.Player.OnJump -= OnJump;
        //점프 홀딩 이벤트 구독
        InputEvents.Player.OnJumpHold -= OnJumpHold;
        //대쉬 이벤트 구독
        InputEvents.Player.OnDash -= OnDash;
        //패링 이벤트 구독
        InputEvents.Player.OnParry -= OnParry;
        //ESC 이벤트 (시스템 메뉴 열기) 구독
        InputEvents.Player.OnCancel -= OnCancel;
        //Interact 이벤트 구독
        InputEvents.Player.OnInteract -= OnInteract;


        //세이브슬롯에서 게임씬으로 로드가 완료되었을 때
        MapEvents.OnSavedSceneLoaded -= OnSavedSceneLoaded;
    }
    //점프 이벤트 구독
    void OnJump(bool j)
    {
        if (j)
        {
            isJumpEvent = true;
        }
        else
        {
            isJumpEvent = false;
        }
    }
    //점프 홀딩 이벤트 구독
    void OnJumpHold(bool j_Hold)
    {
        if (j_Hold)
        {
            isJumpHoldEvent = true;
        }
        else
        {
            isJumpHoldEvent = false;
        }
    }
    //대쉬 이벤트 구독
    void OnDash(bool d)
    {
        if (d)
        {
            isDashEvent = true;
            if (!isDashCoolTime && FSM.currentState.CanChangeState(FSM.dashState))
                FSM.ChangeState(FSM.dashState);
        }
        else
        {
            isDashEvent = false;
        }
    }
    //패링 이벤트 구독
    void OnParry(bool p)
    {
        if (p)
        {
            isParryEvent = true;
            if (!isParryCoolTime && FSM.currentState.CanChangeState(FSM.parryState))
                FSM.ChangeState(FSM.parryState);
        }
        else
        {
            isParryEvent = false;
        }
    }

    //ESC 이벤트 (시스템 메뉴 열기) 구독
    void OnCancel(bool esc)
    {
    }
    //Interact 이벤트 구독
    void OnInteract(bool e)
    {
    }

    //임시 사용
    void OnSavedSceneLoaded()
    {
        //플레이어 위치 초기화
        transform.position = player.savePosition + new Vector2(1,1);
    }



}
