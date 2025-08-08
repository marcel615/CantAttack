using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //�� ������Ʈ
    public EnemyFSM FSM;
    public EnemyReactionHandler reactionHandler;

    //�� �ڽ� ������Ʈ
    Transform groundCheckFront;
    Transform wallCheckFront;
    public Transform MeleeAttack1Point;

    //��ü ������ ������ �ִ� DataSO
    [SerializeField] private EnemyDataSO enemyDataSO;

    //�� �ؿ� ���� �ִ��� üũ ���� ������
    float checkRadius = 0.1f;
    LayerMask groundLayer;

    //Enemy �⺻ ������
    [HideInInspector] public int MaxHP;
    [HideInInspector] public int CurrentHP;
    [HideInInspector] public float isHeadToRight; //ĳ���Ͱ� �ٶ󺸴� ����(1�̸� ������, -1�̸� ����)

    //Idle ���� ����
    [HideInInspector] public float idleMinWaitTime;
    [HideInInspector] public float idleMaxWaitTime;

    //���� ���� ����
    [HideInInspector] public float patrolSpeed;
    [HideInInspector] public float patrolMinDistance;
    [HideInInspector] public float patrolMaxDistance;

    //�ǰݵǾ��� �� ���� ����    
    [HideInInspector] public float hitColorTime;  //�ǰ� �� ���� �ð�    
    [HideInInspector] public bool isKnockbackEnable;     //�˹� �������� �÷���
    [HideInInspector] public float knockbackPower;
    [HideInInspector] public bool isKnockbacked;         //�˹� ���ߴ��� �÷��� (�ٸ� ������ ��� ����)
    [HideInInspector] public float knockbackCantMoveTime;  //�˹����� ��õ��� ������ ���� �ð�

    //�÷��̾� �������� �� ���� ����
    [HideInInspector] public bool isPlayerDetected;
    [HideInInspector] public GameObject player;
    [HideInInspector] public float chaseSpeed;

    //���� ���� ����
    [HideInInspector] public bool isAttackEnable;
    [HideInInspector] public int attackDamage;
    [HideInInspector] public float attackTime;
    [HideInInspector] public float attackWaitTime;
    [HideInInspector] public GameObject slashEffectPrefab;

    //���� ���� ����
    [HideInInspector] public bool isParryStun;
    [HideInInspector] public float parryStunTime;

    //���� ���� ����
    [HideInInspector] public bool isDead;
    [HideInInspector] public Sprite deadImage;



    private void Awake()
    {
        FSM = GetComponent<EnemyFSM>();
        reactionHandler = GetComponent<EnemyReactionHandler>();

        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (groundCheckFront == null) groundCheckFront = transform.Find("GroundCheckFront")?.GetComponent<Transform>();
        if (wallCheckFront == null) wallCheckFront = transform.Find("WallCheckFront")?.GetComponent<Transform>();

        //�� üũ ����
        groundLayer = LayerMask.GetMask("Ground", "");

        //EnemyDataSO�� ������ �ʱ�ȭ �۾�
        MaxHP = enemyDataSO.MaxHP;
        CurrentHP = enemyDataSO.CurrentHP;
        isHeadToRight = 1f; //ĳ���Ͱ� �ٶ󺸴� ����(1�̸� ������, -1�̸� ����)

        //Idle ���� ����
        idleMinWaitTime = enemyDataSO.idleMinWaitTime;
        idleMaxWaitTime = enemyDataSO.idleMaxWaitTime;

        //���� ���� ����
        patrolSpeed = enemyDataSO.patrolSpeed;
        patrolMinDistance = enemyDataSO.patrolMinDistance;
        patrolMaxDistance = enemyDataSO.patrolMaxDistance;

        //�ǰݵǾ��� �� ���� ����    
        hitColorTime = 0.3f;  //�ǰ� �� ���� �ð�    
        isKnockbackEnable = enemyDataSO.isKnockbackEnable;     //�˹� �������� �÷���
        knockbackPower = 15f;
        isKnockbacked = false;         //�˹� ���ߴ��� �÷��� (�ٸ� ������ ��� ����)
        knockbackCantMoveTime = 0.1f;  //�˹����� ��õ��� ������ ���� �ð�

        //�÷��̾� �������� �� ���� ����
        isPlayerDetected = false;
        player = null;
        chaseSpeed = enemyDataSO.chaseSpeed;

        //���� ���� ����
        isAttackEnable = false;
        attackDamage = enemyDataSO.attackDamage;
        attackTime = enemyDataSO.attackTime;
        attackWaitTime = enemyDataSO.attackWaitTime;
        slashEffectPrefab = enemyDataSO.slashEffectPrefab;

        //���� ���� ����
        isParryStun = false;
        parryStunTime = 3f;

        //���� ���� ����
        isDead = false;
        deadImage = enemyDataSO.deadImage;
    }   

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //EnemyHitBox���� Hit�Ǿ��� ��
        EnemyEvents.OnEnemyHitBoxHitted_EnemyDamageHandler += OnDamaged;
        //EnemyPlayerDetector���� �÷��̾ �������� ��
        EnemyEvents.OnEnemyPlayerDetected += OnPlayerDetect;
        //EnemyChaseRange���� �÷��̾ �������� Exit���� ��
        EnemyEvents.OnEnemyChaseOver += OnEnemyChaseOver;
        //EnemyAttackTrigger���� �÷��̾ �������� ��
        EnemyEvents.OnEnemyAttackTriggerEnter += OnAttackTriggerEnter;
        //EnemyAttackTrigger���� �÷��̾ �������� Exit���� ��
        EnemyEvents.OnEnemyAttackTriggerExit += OnAttackTriggerExit;
        //EnemyAttackHitBox���� �÷��̾ �и����� ��
        EnemyEvents.OnEnemyAttackParried += OnAttackParried;
    }
    private void OnDisable()
    {
        //EnemyHitBox���� Hit�Ǿ��� ��
        EnemyEvents.OnEnemyHitBoxHitted_EnemyDamageHandler -= OnDamaged;
        //EnemyPlayerDetector���� �÷��̾ �������� ��
        EnemyEvents.OnEnemyPlayerDetected -= OnPlayerDetect;
        //EnemyChaseRange���� �÷��̾ �������� Exit���� ��
        EnemyEvents.OnEnemyChaseOver -= OnEnemyChaseOver;
        //EnemyAttackTrigger���� �÷��̾ �������� ��
        EnemyEvents.OnEnemyAttackTriggerEnter -= OnAttackTriggerEnter;
        //EnemyAttackTrigger���� �÷��̾ �������� Exit���� ��
        EnemyEvents.OnEnemyAttackTriggerExit -= OnAttackTriggerExit;
        //EnemyAttackHitBox���� �÷��̾ �и����� ��
        EnemyEvents.OnEnemyAttackParried -= OnAttackParried;
    }
    //�ǰ� �̺�Ʈ �߻� ��
    void OnDamaged(Vector2 hitTargetPos, int damage)
    {
        //������ �����Ű��
        CurrentHP -= damage;

        //���� ü�¿� ���� �˹� Ȥ�� FSM State ��ȯ
        if (CurrentHP > 0)
        {
            if (isKnockbackEnable)
            {
                reactionHandler.HitWithKnockback(hitColorTime, hitTargetPos);
            }
            else
            {
                reactionHandler.HitWithoutKnockback(hitColorTime);
            }
        }
        else
        {
            //���� ���·� ��ȯ�Ǵ��� üũ�ϰ� ��ȯ�ϵ���
            if (FSM.CanChangeState(FSM.deadState))
                FSM.ChangeState(FSM.deadState);
        }
    }

    void OnPlayerDetect(GameObject P)
    {
        isPlayerDetected = true;
        player = P;

        //���� ���·� ��ȯ�Ǵ��� üũ�ϰ� ��ȯ�ϵ���
        if (FSM.CanChangeState(FSM.chaseState))
            FSM.ChangeState(FSM.chaseState);
    }
    void OnEnemyChaseOver(GameObject P)
    {
        isPlayerDetected = false;
        player = null;

        //���� ���·� ��ȯ�Ǵ��� üũ�ϰ� ��ȯ�ϵ���
        if (FSM.CanChangeState(FSM.idleState))
            FSM.ChangeState(FSM.idleState);
    }
    void OnAttackTriggerEnter(GameObject P)
    {
        isAttackEnable = true;

        //���� ���·� ��ȯ�Ǵ��� üũ�ϰ� ��ȯ�ϵ���
        if (FSM.CanChangeState(FSM.attackState))
            FSM.ChangeState(FSM.attackState);
    }
    void OnAttackTriggerExit(GameObject P)
    {
        isAttackEnable = false;
    }
    void OnAttackParried()
    {
        isParryStun = true;

        //���� ���·� ��ȯ�Ǵ��� üũ�ϰ� ��ȯ�ϵ���
        if (FSM.CanChangeState(FSM.stunState))
            FSM.ChangeState(FSM.stunState);
    }

    //�տ� ���� �ִ��� üũ�ϴ� �޼��� (���� ������ true, ������ false ��ȯ)
    public bool isGroundFront()
    {
        return Physics2D.Raycast(groundCheckFront.position, Vector2.down, checkRadius, groundLayer);
    }
    //�տ� ���� �ִ��� üũ�ϴ� �޼��� (���� ������ true, ������ false ��ȯ)
    public bool isWallFront()
    {
        Vector2 checkDir = new Vector2(isHeadToRight, 0);
        return Physics2D.Raycast(wallCheckFront.position, checkDir, checkRadius, groundLayer);
    }

}
