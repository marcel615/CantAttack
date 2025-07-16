using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //내 컴포넌트
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    //다른 객체들
    public CameraManager mainCamera;

    //키 입력 추적 변수들
    float H; //좌우
    bool J; //점프
    bool J_ing; //점프버튼 누르고 있는지
    bool D; //대쉬
    bool P; //패링

    //기본 변수들
    float normalSpeed = 6f;
    float jumpPower = 10f;

    //발 밑에 땅이 있는지 체크
    bool isGrounded;
    Transform groundCheckObj;          
    float checkRadius = 0.2f;
    LayerMask groundLayer;
    

    //점프 관련 변수들
    bool isJumping;
    float MaxJumpTime = 0.3f;
    float MaxJumpTimer;
    int jumpCount = 0;
  

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();     
        
    }
    private void Start()
    {
        //메인 카메라 설정
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
            mainCamera.SetTarget(transform);
        }

        //땅 체크 오브젝트 할당
        groundCheckObj = transform.Find("GroundCheckObject");
        groundLayer = LayerMask.GetMask("Ground");

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

        //H값에 따른 플레이어 캐릭터가 바라보는 방향 및 애니메이션 설정
        if (H != 0)
        {
            spriteRenderer.flipX = H < 0;
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        //J값에 따른 플레이어 점프 && 땅에 서있으면 && jumpCount가 0이면 점프하도록
        if (J && isGrounded && jumpCount == 0)
        {
            rigid.velocity = new Vector2 (rigid.velocity.x, jumpPower);
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


    }
    private void FixedUpdate()
    {
        //플레이어 기본 움직임 설정
        rigid.velocity = new Vector2(H * normalSpeed, rigid.velocity.y);

        //플레이어 캐릭터가 떨어지고 있을 때 작업. 1.애니메이션 변수 조정
        if(rigid.velocity.y < -0.01f)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

    }



}
