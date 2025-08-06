using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <�������>
    /// 
    /// �� üũ
    /// ���� ���� �÷��� üũ
    /// ���� üũ
    /// �÷��̾� ���� ��Ȳ üũ
    /// ���̺� �ε�
    /// </�������>
    
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    public static Player Instance;

    //�� ������Ʈ
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D detectCollider;
    Animator animator;
    public PlayerStatus status;

    //�� �ڽ� ������Ʈ ����
    public Transform groundCheckObj;
    public CapsuleCollider2D playerHitBoxCollider;

    //���̺�, �ε� ����
    public Vector2 savePosition;

    //���ؽ�Ʈ enum ����
    public InputContext playerContext = InputContext.Player;

    //�⺻ ������
    public float normalSpeed = 6f;
    public float normalJumpPower = 10f;
    public float doubleJumpPower = 20f;
    public float isHeadToRight = 1f; //ĳ���Ͱ� �ٶ󺸴� ����(1�̸� ������, -1�̸� ����)

    //���� ���� �÷���
    public bool canControl = true; //���� ���� �÷���
    public bool isJumping;          //���� ���� �÷���
    public bool isDashing;          //ȸ�� ���� �÷���
    public bool isParrying;         //�и� ���� �÷���
    public bool isKnockbacked;      //�ǰ� �� �˹� ���� �÷���
    public bool isGrounded;         //�ٴ� üũ �÷���
    public bool isPortalEnter;      //��Ż ���� �÷���
    public bool isFalling;          //���� ���� �÷���

    //�� �ؿ� ���� �ִ��� üũ ���� ������
    float checkRadius = 0.2f;
    LayerMask groundLayer;

    //���� �� �ǰ� ���� ������
    public bool isInvincible = false;
    public float InvincibleTime_Hitted = 1.5f;
    public float InvincibleTimer;
    public bool isKnockbackInvincible;


    private void Awake()
    {
        // ���� �ν��Ͻ��� ������ �� && ���� ���ο� �ν��Ͻ��� �����Ƿ��� �� ��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //�ߺ����� �ʵ��� ���� ���Ӱ� �����Ǵ� ���� �ı���Ų��
            return;
        }
        // �ν��Ͻ� ó�� �Ҵ�
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //�� ������Ʈ ����
        rigid = GetComponent<Rigidbody2D>();
        detectCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        status = GetComponent<PlayerStatus>();

        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        //HitBox�� Collider ����
        if (playerHitBoxCollider == null) playerHitBoxCollider = transform.Find("HitBox")?.GetComponent<CapsuleCollider2D>();
        //�� üũ ������Ʈ ����
        if (groundCheckObj == null) groundCheckObj = transform.Find("GroundCheckObject")?.GetComponent<Transform>();
        
        groundLayer = LayerMask.GetMask("Ground","");

    }
    private void FixedUpdate()
    {
        //�� ���� �ִ��� üũ
        isGrounded = Physics2D.OverlapCircle(groundCheckObj.position, checkRadius, groundLayer);

        //����� ������ ������ �������� �Ǻ�
        if (!isKnockbacked && !isDashing && !isParrying)
        {
            canControl = true;
        }
        else
        {
            canControl = false;
        }

        //���� �ð� �����ϱ�
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

        //�÷��̾� ĳ���Ͱ� �������� ���� �� �۾�. 1.�ִϸ��̼� ���� ����
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
        //���̺꽽�Կ��� ���Ӿ����� �ε尡 �Ϸ�Ǿ��� ��
        MapEvents.OnSavedSceneLoaded += OnSavedSceneLoaded;
    }
    private void OnDisable()
    {
        //���̺꽽�Կ��� ���Ӿ����� �ε尡 �Ϸ�Ǿ��� ��
        MapEvents.OnSavedSceneLoaded -= OnSavedSceneLoaded;
    }
    public void Init()
    {
        PlayerEvents.InvokePlayerInstance(this);
    }

    //���̺� �ε� ���� �ʱ�ȭ
    void OnSavedSceneLoaded()
    {
        //�÷��̾� ��ġ �ʱ�ȭ
        transform.position = savePosition;

        //�÷��̾� ���� �̺�Ʈ ����
        PlayerEvents.InvokePlayerSpawned_HPUIManager(status.MaxHP, status.CurrentHP);
    }

}
