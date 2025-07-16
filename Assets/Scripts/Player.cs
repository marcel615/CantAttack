using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    //�ٸ� ��ü��
    public CameraManager mainCamera;

    //Ű �Է� ���� ������
    float H; //�¿�
    bool J; //����
    bool J_ing; //������ư ������ �ִ���
    bool D; //�뽬
    bool P; //�и�

    //�⺻ ������
    float normalSpeed = 6f;
    float jumpPower = 10f;

    //�� �ؿ� ���� �ִ��� üũ
    bool isGrounded;
    Transform groundCheckObj;          
    float checkRadius = 0.2f;
    LayerMask groundLayer;
    

    //���� ���� ������
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
        //���� ī�޶� ����
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
            mainCamera.SetTarget(transform);
        }

        //�� üũ ������Ʈ �Ҵ�
        groundCheckObj = transform.Find("GroundCheckObject");
        groundLayer = LayerMask.GetMask("Ground");

    }
    private void Update()
    {
        //����� �Էµ� �޾ƿ���
        H = Input.GetAxisRaw("Horizontal");
        J = Input.GetButtonDown("Jump");
        J_ing = Input.GetButton("Jump");
        D = Input.GetButtonDown("Dash");
        P = Input.GetButtonDown("Parry");

        //�� ���� �ִ��� üũ
        isGrounded = Physics2D.OverlapCircle(groundCheckObj.position, checkRadius, groundLayer);

        //H���� ���� �÷��̾� ĳ���Ͱ� �ٶ󺸴� ���� �� �ִϸ��̼� ����
        if (H != 0)
        {
            spriteRenderer.flipX = H < 0;
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        //J���� ���� �÷��̾� ���� && ���� �������� && jumpCount�� 0�̸� �����ϵ���
        if (J && isGrounded && jumpCount == 0)
        {
            rigid.velocity = new Vector2 (rigid.velocity.x, jumpPower);
            jumpCount = 1;
            MaxJumpTimer = MaxJumpTime;
            isJumping = true;

            animator.SetTrigger("isJump"); //�ִϸ��̼� ���� ����

        }
        //1�� ���� �������� ����Ű�� ������ �ִ� ���� ���� ���� ���������� 
        if (J_ing && isJumping)
        {
            if (jumpCount == 1) //1�� ���� ����
            {
                if (MaxJumpTimer > 0) //���� ���� ���� �ɱ�
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

        // ���� �� ���� �����ϸ� �ٽ� jumpCount �ʱ�ȭ, �ִϸ��̼� ���� ����
        if (!isJumping && jumpCount != 0 && isGrounded)
        {
            jumpCount = 0;
        }


    }
    private void FixedUpdate()
    {
        //�÷��̾� �⺻ ������ ����
        rigid.velocity = new Vector2(H * normalSpeed, rigid.velocity.y);

        //�÷��̾� ĳ���Ͱ� �������� ���� �� �۾�. 1.�ִϸ��̼� ���� ����
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
