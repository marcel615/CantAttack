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
    public GameObject player;
    public float chaseSpeed;



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
    }
    private void OnDisable()
    {
        //EnemyHitBox���� Hit�Ǿ��� ��
        EnemyEvents.OnEnemyHitBoxHitted_EnemyDamageHandler -= OnDamaged;
        //EnemyPlayerDetector���� �÷��̾ �������� ��
        EnemyEvents.OnEnemyPlayerDetected -= OnPlayerDetect;
        //EnemyChaseRange���� �÷��̾ �������� Exit���� ��
        EnemyEvents.OnEnemyChaseOver -= OnEnemyChaseOver;
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
        player = P;
        FSM.ChangeState(FSM.chaseState);
    }
    void OnEnemyChaseOver(GameObject P)
    {
        player = null;
        FSM.ChangeState(FSM.idleState);
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
