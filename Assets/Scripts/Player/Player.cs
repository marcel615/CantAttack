using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    private static Player Instance;

    //세이브, 로드 변수
    public Vector2 savePosition;

    //내 컴포넌트
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public PlayerStatus status;

    //내 자식 오브젝트 관련
    public CapsuleCollider2D playerHitBoxCollider;
    public Transform groundCheckObj;


    //키 입력 추적 변수들
    float H; //좌우
    bool J; //점프
    bool J_ing; //점프버튼 누르고 있는지
    bool D; //대쉬
    bool P; //패링

    //기본 변수들
    float normalSpeed = 6f;
    float jumpPower = 10f;
    float isHeadToRight = 1; //캐릭터가 바라보는 방향(1이면 오른쪽, -1이면 왼쪽)
    float prevGravity;

    //조작 제한 플래그
    bool canControl = true;

    //발 밑에 땅이 있는지 체크 관련 변수들
    bool isGrounded;
    float checkRadius = 0.2f;
    LayerMask groundLayer;

    //무적 및 피격 관련 변수들
    bool isInvincible = false;
    float InvincibleTime_Hitted = 1.5f;
    float InvincibleTimer;

    bool isKnockedBackInvincible;
    bool isKnockedBack = false; //조작 제한 플래그 too
    float KnockedBackTime = 0.3f;
    float KnockedBackTimer;

    //점프 관련 변수들
    bool isJumping;
    float MaxJumpTime = 0.3f;
    float MaxJumpTimer;
    int jumpCount = 0;

    //회피 관련 변수들
    bool isDashing;  //조작 제한 플래그 too
    float DashTime = 0.4f;
    float DashTimer;
    bool isDashCoolTime;    //회피 쿨타임
    float DashCoolTime = 1f;
    float DashCoolTimer;
    float DashSpeed = 12f;




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
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        status = GetComponent<PlayerStatus>();


        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        //HitBox의 Collider 연결
        if (playerHitBoxCollider == null) playerHitBoxCollider = transform.Find("HitBox")?.GetComponent<CapsuleCollider2D>();
        //땅 체크 오브젝트 연결
        if (groundCheckObj == null) groundCheckObj = transform.Find("GroundCheckObject")?.GetComponent<Transform>();
        
        groundLayer = LayerMask.GetMask("Ground");

    }
    private void Start()
    {

    }
    private void Update()
    {
        //사용자 입력들 받아오기
        H = Input.GetAxisRaw("Horizontal");
        J = Input.GetButtonDown("Jump");
        J_ing = Input.GetButton("Jump");
        D = Input.GetButtonDown("Dash");
        P = Input.GetButtonDown("Parry");

        //땅 위에 있는지 체크
        isGrounded = Physics2D.OverlapCircle(groundCheckObj.position, checkRadius, groundLayer);

        //사용자 조작이 가능한 상태인지 판별
        if (!isKnockedBack && !isDashing)
        {
            canControl = true;
        }
        else
        {
            canControl = false;
        }

        //H값에 따른 플레이어 캐릭터가 바라보는 방향 및 애니메이션 설정
        if (H != 0 && canControl)
        {
            spriteRenderer.flipX = H < 0;
            animator.SetBool("isMoving", true);
            isHeadToRight = (H > 0) ? 1 : -1; //H가 양수면 1 저장, 아니면 -1 저장
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        //무적 시간 제어하기
        if (isInvincible)
        {
            if(InvincibleTimer > 0)
            {
                playerHitBoxCollider.enabled = false;
                InvincibleTimer -= Time.deltaTime;
            }
            else
            {
                playerHitBoxCollider.enabled = true;
                InvincibleTimer = 0;
                isInvincible = false;
                if (isKnockedBackInvincible)
                {
                    isKnockedBackInvincible = false;
                    spriteRenderer.color = new Color(1, 1, 1, 1f); //투명해졌던거 다시 원상복귀                    
                }
            }
        }

        //넉백 시간 제어하기
        if (isKnockedBack)
        {
            if (KnockedBackTimer > 0)
            {
                KnockedBackTimer -= Time.deltaTime;
            }
            else
            {
                KnockedBackTimer = 0;
                isKnockedBack = false;
            }
        }

        //J값에 따른 플레이어 점프 && 땅에 서있으면 && jumpCount가 0이면 점프하도록
        if (J && isGrounded && jumpCount == 0 && canControl)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
            jumpCount = 1;
            MaxJumpTimer = MaxJumpTime;
            isJumping = true;

            animator.SetTrigger("isJump"); //애니메이션 변수 설정

        }
        //1단 점프 한정으로 점프키를 누르고 있는 동안 점프 높이 높아지도록 
        if (J_ing && isJumping)
        {
            if (jumpCount == 1) //1단 점프 한정
            {
                if (MaxJumpTimer > 0) //점프 높이 제약 걸기
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
                    MaxJumpTimer -= Time.deltaTime;

                }
                else
                {
                    MaxJumpTimer = 0;
                    isJumping = false;
                }
            }
        }
        else
        {
            MaxJumpTimer = 0;
            isJumping = false;
        }

        // 점프 후 땅에 도달하면 다시 jumpCount 초기화, 애니메이션 변수 설정
        if (!isJumping && jumpCount != 0 && isGrounded)
        {
            jumpCount = 0;
        }

        //D값에 따른 플레이어 회피기
        if (D && !isDashCoolTime && canControl)
        {
            DashCoolTimer = DashCoolTime;
            isDashCoolTime = true;

            DashTimer = DashTime;
            isDashing = true;
            InvincibleTimer = DashTime;
            isInvincible = true;

            prevGravity = rigid.gravityScale;
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(isHeadToRight * DashSpeed, 0);
        }
        if (isDashing)
        {
            if(DashTimer > 0)
            {
                rigid.velocity = new Vector2(isHeadToRight * DashSpeed, 0);
                DashTimer -= Time.deltaTime;
            }
            else
            {
                DashTimer = 0;
                isDashing = false;

                rigid.gravityScale = prevGravity;
            }
        }
        //회피기 쿨타임 계산
        if (isDashCoolTime)
        {
            if (DashCoolTimer > 0)
            {
                DashCoolTimer -= Time.deltaTime;
            }
            else
            {
                DashCoolTimer = 0; 
                isDashCoolTime = false;
            }
        }


    }
    private void FixedUpdate()
    {
        //플레이어 기본 움직임 설정
        if (canControl)
        {
            rigid.velocity = new Vector2(H * normalSpeed, rigid.velocity.y);
        }

        //플레이어 캐릭터가 떨어지고 있을 때 작업. 1.애니메이션 변수 조정
        if (rigid.velocity.y < -0.01f)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

    }
    private void OnEnable()
    {
        //세이브 로드 이후 초기화
        SystemEvents.OnDataLoadFinished += InitFromSave;
    }
    private void OnDisable()
    {
        //세이브 로드 이후 초기화
        SystemEvents.OnDataLoadFinished -= InitFromSave;
    }

    //세이브 로드 이후 초기화
    void InitFromSave()
    {
        //플레이어 위치 초기화
        transform.position = savePosition;

        //플레이어 스폰 이벤트 발행
        PlayerEvents.InvokePlayerSpawned(transform, status.MaxHP, status.CurrentHP);
    }

    // 데미지를 입었을 때 이 메소드 호출
    public void OnDamaged(Vector2 hitPosition, int damage)
    {
        if (!isInvincible)
        {

            //status에 데미지 적용 후 이벤트 발행
            DamageApply(damage);
            PlayerEvents.InvokePlayerDamaged(status.MaxHP, status.CurrentHP);

            //무적 타이머, 넉백 타이머 실행
            isInvincible = true;
            InvincibleTimer = InvincibleTime_Hitted;

            isKnockedBackInvincible = true;
            isKnockedBack = true;
            KnockedBackTimer = KnockedBackTime;

            //넉백 구현
            if (transform.position.x < hitPosition.x)
            {
                rigid.AddForce(new Vector2(-0.5f, 1f) * 15, ForceMode2D.Impulse);
            }
            else
            {
                rigid.AddForce(new Vector2(0.5f, 1f) * 15, ForceMode2D.Impulse);

            }
            spriteRenderer.color = new Color(1, 1, 1, 0.4f); //피격 시 반투명하게 됨
        }

    }
    //데미지 적용시키는 메소드
    void DamageApply(int damage)
    {
        if(status.CurrentHP - damage > 0)
        {
            status.CurrentHP -= damage;
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
