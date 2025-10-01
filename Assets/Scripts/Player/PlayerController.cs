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
    PlayerFSM FSM;

    //내 자식 오브젝트 관련
    public Transform groundCheckObj;
    public CapsuleCollider2D playerHitBoxCollider;
    public Transform cameraFollowTransform;

    //컨텍스트 enum 정보
    public InputContext thisContext = InputContext.Player;

    //세이브, 로드 변수
    public int MaxHP;
    public int CurrentHP;
    public Vector2 savePosition;
    public bool isDoubleJumpUnlocked;

    //상태 관련 정보

    //무적 관련 변수
    public bool isInvincible;
    public float InvincibleTimer;

    //땅 위인지 체크 관련 변수
    public bool isGrounded;         //바닥 체크 플래그

    //Move 관련 변수
    public bool isMoveInput;        //Horizontal 입력이 들어오고 있는지 플래그
    public float H;
    public float normalSpeed = 6f;
    public float isHeadToRight = 1f; //캐릭터가 바라보는 방향(1이면 오른쪽, -1이면 왼쪽)

    //점프 관련 변수
    public bool isJumpEvent;
    public bool isJumpHoldEvent;
    public bool isJumping;          //점프 관련 플래그
    public int jumpCount;           //점프 횟수 변수
    public float MaxJumpTime = 0.35f;
    public float normalJumpPower = 10f;
    public float doubleJumpPower = 20f;

    //낙하 상태인지 체크 관련 변수
    public bool isFalling;          //낙하 상태 플래그

    //대시 관련 변수
    public bool isDashing;          //회피 관련 플래그
    public bool isDashedInAir;      //공중에서 이미 대시했는지 여부
    public float dashSpeed = 12f;
    public float dashTime = 0.4f;
    public float dashCoolTimer;
    public float dashCoolTime = 1f;
    public bool isDashCoolTime;

    //패리 관련 변수
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
    public Vector2 hittedPos;
    public int hittedDamage;
    public float knockbackTime = 0.3f;
    public float knockbackPower = 15f;
    public float InvincibleTime_Hitted = 1.5f;
    public bool isKnockbackInvincible;

    //사망 관련 변수
    public float deadSequenceTime = 2f;
    public float deadSlowMotionTime = 1.2f;
    public float deadSlowMotionTimeScale = 0.2f;
    public GameObject bloodEffectPrefab;

    //스폰 관련
    public bool isSaveSceneLoaded;
    public bool isRespawned;

    //포탈 이동 관련 변수
    public float portalMoveTime = 0.3f;
    public PortalWalkDirection walkDir;    //포탈 무브 방향

    //상호작용 관련
    public IInteractable interactableTarget;


    //발 밑에 땅이 있는지 체크 관련 변수들
    LayerMask groundLayer;
    Vector2 checkSize;


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
        spriteRenderer = GetComponent<SpriteRenderer>();
        FSM = GetComponent<PlayerFSM>();

        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        //HitBox의 Collider 연결
        if (playerHitBoxCollider == null) playerHitBoxCollider = transform.Find("HitBox")?.GetComponent<CapsuleCollider2D>();
        //땅 체크 오브젝트 연결
        if (groundCheckObj == null) groundCheckObj = transform.Find("GroundCheckObject")?.GetComponent<Transform>();
        
        //땅 체크 관련
        groundLayer = LayerMask.GetMask("Ground");
        checkSize = new Vector2(0.85f, 0.02f);
    }
    private void Update()
    {
        //무적 관련
        if (isInvincible)
        {
            playerHitBoxCollider.enabled = false;
        }
        else
        {
            playerHitBoxCollider.enabled = true;

            //넉백으로 인한 무적이었다면 반투명하게 되던거 원상복구
            if (isKnockbackInvincible)
            {
                isKnockbackInvincible = false;
                spriteRenderer.color = new Color(1, 1, 1, 1);
            }
        }

        //Move 관련
        if (H != 0) isMoveInput = true;
        else isMoveInput = false;

        //땅 위에 있는지 체크
        isGrounded = Physics2D.OverlapBox(groundCheckObj.position, checkSize, 0f, groundLayer);
        if (isGrounded)
        {
            if (FSM.currentState.CanChangeState(FSM.groundState))
                FSM.ChangeState(FSM.groundState);

            //땅에 닿으면 공중대시 플래그 초기화 
            //나중에 이런 플래그들은 몽땅 다른 곳에서 컨트롤하자
            isDashedInAir = false;
            isParriedInAir = false;
        }
        //낙하 상태인지 체크
        if (rigid.velocity.y < -0.1f)
        {
            isFalling = true;
            if (FSM.currentState.CanChangeState(FSM.inAirState))
                FSM.ChangeState(FSM.inAirState);
        }
        else
        {
            isFalling = false;
        }
    }
    //점프 입력 이벤트
    public void OnJump(bool j)
    {
        isJumpEvent = j;
    }
    //점프 홀딩 입력 이벤트
    public void OnJumpHold(bool j_Hold)
    {
        isJumpHoldEvent = j_Hold;
    }
    //대시 입력 이벤트
    public void OnDash(bool d)
    {
        if (!isDashedInAir && !isDashCoolTime && FSM.currentState.CanChangeState(FSM.dashState))
            FSM.ChangeState(FSM.dashState);
    }
    //패링 입력 이벤트
    public void OnParry(bool p)
    {
        if (!isParriedInAir && !isParryCoolTime && FSM.currentState.CanChangeState(FSM.parryState))
            FSM.ChangeState(FSM.parryState);
    }

    //ESC 입력 이벤트 (시스템 메뉴 열기)
    public void OnCancel(bool esc)
    {
        //SystemMenu 오픈
        InputEvents.SystemMenu.InvokeSystemMenuOpen(thisContext);

        //이 때 튜토리얼이 열려있었다면 닫도록 해야 하기 때문에 실행
        TutorialEvents.InvokeTutorialClose();
    }
    //Interact 입력 이벤트
    public void OnInteract(bool e)
    {
        if (interactableTarget == null) return;
        if (interactableTarget.IsInteractable() && FSM.currentState.CanChangeState(FSM.interactionState))
            FSM.ChangeState(FSM.interactionState);
    }

    //Tab 입력 이벤트 (ParryMode 패널 열기)
    public void OnTab(bool tab)
    {
        //ParryMode 오픈
        InputEvents.ParryMode.InvokeParryModeOpen(thisContext);

        //이 때 튜토리얼이 열려있었다면 닫도록 해야 하기 때문에 실행
        TutorialEvents.InvokeTutorialClose();
    }

    //피격 이벤트
    public void OnDamaged(Vector2 hitTargetPos, int damage)
    {
        hittedPos = hitTargetPos;
        hittedDamage = damage;

        if (!isInvincible && FSM.currentState.CanChangeState(FSM.hitState))
            FSM.ChangeState(FSM.hitState);
    }
    //Dead 이벤트
    public void OnPlayerDead()
    {
        if (FSM.currentState.CanChangeState(FSM.deadState))
            FSM.ChangeState(FSM.deadState);
    }
    //세이브파일 로드 이벤트
    public void OnSavedSceneLoaded()
    {
        isSaveSceneLoaded = true;
        FSM.ChangeState(FSM.spawnState);
    }
    //리스폰 이벤트
    public void OnRespawnSceneLoaded()
    {
        isRespawned = true;
        if (FSM.currentState.CanChangeState(FSM.spawnState))
            FSM.ChangeState(FSM.spawnState);
    }
    //포탈 진입 이벤트
    public void OnPortalEnter(string enterP, string targetS, string targetP, PortalWalkDirection walkD)
    {
        walkDir = walkD;
        if (FSM.currentState.CanChangeState(FSM.portalState))
            FSM.ChangeState(FSM.portalState);
    }
    //세이브포인트에서 저장 전 공지 이벤트
    public void SavePlayerPos(SavePointSO savePointSO)
    {
        savePosition = savePointSO.position;
    }
    //더블점프 해금
    public void UnlockDoubleJump()
    {
        isDoubleJumpUnlocked = true;
    }

    //BootInitializer에서 실행하는 초기화 함수
    public void Init()
    {
        PlayerEvents.InvokePlayerControllerInstance(this);
    }
}
