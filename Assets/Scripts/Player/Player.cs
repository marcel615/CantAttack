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
    private static Player Instance;

    //�� ������Ʈ
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public PlayerStatus status;

    //�� �ڽ� ������Ʈ ����
    public CapsuleCollider2D playerHitBoxCollider;
    public Transform groundCheckObj;

    //���̺�, �ε� ����
    public Vector2 savePosition;

    //���ؽ�Ʈ enum ����
    public InputContext playerContext = InputContext.Player;

    //�⺻ ������
    public float normalSpeed = 6f;
    public float normaljumpPower = 10f;
    public float isHeadToRight = 1; //ĳ���Ͱ� �ٶ󺸴� ����(1�̸� ������, -1�̸� ����)

    //���� ���� �÷���
    public bool canControl = true; //���� ���� �÷���
    public bool isJumping; //���� ���� �÷���
    public bool isDashing; //ȸ�� ���� �÷���
    public bool isKnockedBack; //�ǰ� �� �˹� ���� �÷���
    public bool isGrounded; //�ٴ� üũ �÷���

    //�� �ؿ� ���� �ִ��� üũ ���� ������
    float checkRadius = 0.2f;
    LayerMask groundLayer;

    //���� �� �ǰ� ���� ������
    public bool isInvincible = false;
    public float InvincibleTime_Hitted = 1.5f;
    public float InvincibleTimer;
    public bool isKnockedBackInvincible;


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
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        status = GetComponent<PlayerStatus>();

        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        //HitBox�� Collider ����
        if (playerHitBoxCollider == null) playerHitBoxCollider = transform.Find("HitBox")?.GetComponent<CapsuleCollider2D>();
        //�� üũ ������Ʈ ����
        if (groundCheckObj == null) groundCheckObj = transform.Find("GroundCheckObject")?.GetComponent<Transform>();
        
        groundLayer = LayerMask.GetMask("Ground");

    }
    private void Update()
    {
        //�� ���� �ִ��� üũ
        isGrounded = Physics2D.OverlapCircle(groundCheckObj.position, checkRadius, groundLayer);

        //����� ������ ������ �������� �Ǻ�
        if (!isKnockedBack && !isDashing)
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
                    PlayerEvents.InvokePlayerKnockedBackInvincibleOver();
                    //spriteRenderer.color = new Color(1, 1, 1, 1f); //������������ �ٽ� ���󺹱�                    
                }
            }
        }


    }
    private void FixedUpdate()
    {
        //�÷��̾� ĳ���Ͱ� �������� ���� �� �۾�. 1.�ִϸ��̼� ���� ����
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
        //���̺� �ε� ���� �ʱ�ȭ
        SystemEvents.OnDataLoadFinished += InitFromSave;
    }
    private void OnDisable()
    {
        //���̺� �ε� ���� �ʱ�ȭ
        SystemEvents.OnDataLoadFinished -= InitFromSave;
    }

    //���̺� �ε� ���� �ʱ�ȭ
    void InitFromSave()
    {
        //�÷��̾� ��ġ �ʱ�ȭ
        transform.position = savePosition;

        //�÷��̾� ���� �̺�Ʈ ����
        PlayerEvents.InvokePlayerSpawned(transform, status.MaxHP, status.CurrentHP);
    }
}
