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

    //�� �ؿ� ���� �ִ��� üũ ���� ������
    float checkRadius = 0.1f;
    LayerMask groundLayer;

    //Enemy �⺻ ������
    public int MaxHP = 10;
    public int CurrentHP;
    public float isHeadToRight = 1f; //ĳ���Ͱ� �ٶ󺸴� ����(1�̸� ������, -1�̸� ����)

    //Idle���� ������ �� ���� ����
    public float patrolSpeed;
    public float patrolMinDistance;
    public float patrolMaxDistance;
    public float patrolMinWaitTime;
    public float patrolMaxWaitTime;

    //�ǰݵǾ��� �� ���� ����    
    public float hitColorTime = 0.3f;  //�ǰ� �� ���� �ð�    
    public bool isKnockbackEnable;     //�˹� �������� �÷���
    public float knockbackPower = 15f;
    public bool isKnockbacked;         //�˹� ���ߴ��� �÷��� (�ٸ� ������ ��� ����)
    public float knockbackCantMoveTime = 0.1f;  //�˹����� ��õ��� ������ ���� �ð�

    //�÷��̾� �������� �� ���� ����
    public bool isPlayerDetected;
    public GameObject player;
    public float chaseSpeed;

    //���� ���� ����
    public bool isAttackEnable;
    public int attackDamage;
    public float attackTime;
    public float attackWaitTime;

    //���� ���� ����
    public bool isParryStun;
    public float parryStunTime;

    //���� ���� ����
    public bool isDead;



    private void Awake()
    {
        FSM = GetComponent<EnemyFSM>();
        reactionHandler = GetComponent<EnemyReactionHandler>();

        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        //�տ� ���� �ִ��� üũ�ϴ� ������Ʈ ����
        if (groundCheckFront == null) groundCheckFront = transform.Find("GroundCheckFront")?.GetComponent<Transform>();
        //�տ� �����ִ��� üũ�ϴ� ������Ʈ ����
        if (wallCheckFront == null) wallCheckFront = transform.Find("WallCheckFront")?.GetComponent<Transform>();

        //�� üũ ����
        groundLayer = LayerMask.GetMask("Ground", "");

        CurrentHP = MaxHP;
        isKnockbackEnable = true;
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
