using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    private static Player Instance;

    //���̺�, �ε� ����
    public Vector2 savePosition;

    //�� ������Ʈ
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public PlayerStatus status;

    //�� �ڽ� ������Ʈ ����
    public CapsuleCollider2D playerHitBoxCollider;
    public Transform groundCheckObj;


    //Ű �Է� ���� ������
    float H; //�¿�
    bool J; //����
    bool J_ing; //������ư ������ �ִ���
    bool D; //�뽬
    bool P; //�и�

    //�⺻ ������
    float normalSpeed = 6f;
    float jumpPower = 10f;
    float isHeadToRight = 1; //ĳ���Ͱ� �ٶ󺸴� ����(1�̸� ������, -1�̸� ����)
    float prevGravity;

    //���� ���� �÷���
    bool canControl = true;

    //�� �ؿ� ���� �ִ��� üũ ���� ������
    bool isGrounded;
    float checkRadius = 0.2f;
    LayerMask groundLayer;

    //���� �� �ǰ� ���� ������
    bool isInvincible = false;
    float InvincibleTime_Hitted = 1.5f;
    float InvincibleTimer;

    bool isKnockedBackInvincible;
    bool isKnockedBack = false; //���� ���� �÷��� too
    float KnockedBackTime = 0.3f;
    float KnockedBackTimer;

    //���� ���� ������
    bool isJumping;
    float MaxJumpTime = 0.3f;
    float MaxJumpTimer;
    int jumpCount = 0;

    //ȸ�� ���� ������
    bool isDashing;  //���� ���� �÷��� too
    float DashTime = 0.4f;
    float DashTimer;
    bool isDashCoolTime;    //ȸ�� ��Ÿ��
    float DashCoolTime = 1f;
    float DashCoolTimer;
    float DashSpeed = 12f;




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
    private void Start()
    {

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

        //����� ������ ������ �������� �Ǻ�
        if (!isKnockedBack && !isDashing)
        {
            canControl = true;
        }
        else
        {
            canControl = false;
        }

        //H���� ���� �÷��̾� ĳ���Ͱ� �ٶ󺸴� ���� �� �ִϸ��̼� ����
        if (H != 0 && canControl)
        {
            spriteRenderer.flipX = H < 0;
            animator.SetBool("isMoving", true);
            isHeadToRight = (H > 0) ? 1 : -1; //H�� ����� 1 ����, �ƴϸ� -1 ����
        }
        else
        {
            animator.SetBool("isMoving", false);
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
                    spriteRenderer.color = new Color(1, 1, 1, 1f); //������������ �ٽ� ���󺹱�                    
                }
            }
        }

        //�˹� �ð� �����ϱ�
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

        //J���� ���� �÷��̾� ���� && ���� �������� && jumpCount�� 0�̸� �����ϵ���
        if (J && isGrounded && jumpCount == 0 && canControl)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
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

        //D���� ���� �÷��̾� ȸ�Ǳ�
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
        //ȸ�Ǳ� ��Ÿ�� ���
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
        //�÷��̾� �⺻ ������ ����
        if (canControl)
        {
            rigid.velocity = new Vector2(H * normalSpeed, rigid.velocity.y);
        }

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

    // �������� �Ծ��� �� �� �޼ҵ� ȣ��
    public void OnDamaged(Vector2 hitPosition, int damage)
    {
        if (!isInvincible)
        {

            //status�� ������ ���� �� �̺�Ʈ ����
            DamageApply(damage);
            PlayerEvents.InvokePlayerDamaged(status.MaxHP, status.CurrentHP);

            //���� Ÿ�̸�, �˹� Ÿ�̸� ����
            isInvincible = true;
            InvincibleTimer = InvincibleTime_Hitted;

            isKnockedBackInvincible = true;
            isKnockedBack = true;
            KnockedBackTimer = KnockedBackTime;

            //�˹� ����
            if (transform.position.x < hitPosition.x)
            {
                rigid.AddForce(new Vector2(-0.5f, 1f) * 15, ForceMode2D.Impulse);
            }
            else
            {
                rigid.AddForce(new Vector2(0.5f, 1f) * 15, ForceMode2D.Impulse);

            }
            spriteRenderer.color = new Color(1, 1, 1, 0.4f); //�ǰ� �� �������ϰ� ��
        }

    }
    //������ �����Ű�� �޼ҵ�
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

    //�״� �޼ҵ�(���� �̱���)
    void Die()
    {

    }



}
