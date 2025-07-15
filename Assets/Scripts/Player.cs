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
    bool D; //대쉬
    bool P; //패링

    //기본 변수들
    float normalSpeed = 4f;

  

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
    }
    private void Update()
    {
        //사용자 입력들 받아오기
        H = Input.GetAxisRaw("Horizontal");
        J = Input.GetButtonDown("Jump");
        D = Input.GetButtonDown("Dash");
        P = Input.GetButtonDown("Parry");

        //H값에 따른 플레이어 캐릭터가 바라보는 방향 설정
        if(H != 0)
        {
            spriteRenderer.flipX = H < 0;
        }


    }
    private void FixedUpdate()
    {
        //플레이어 기본 움직임 설정
        rigid.velocity = new Vector2(H * normalSpeed, rigid.velocity.y);
        
    }



}
