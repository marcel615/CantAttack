using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //�� ������Ʈ
    public EnemyFSM FSM;
    public EnemyReactionHandler reactionHandler;

    //Enemy �⺻ ������
    public int MaxHP = 10;
    public int CurrentHP;

    //Idle���� ������ �� ���� ����
    public float patrolSpeed;
    public float patrolMinDistance;
    public float patrolMaxDistance;
    public float patrolMinWaitTime;
    public float patrolMaxWaitTime;

    //�ǰݵǾ��� �� ���� ����    
    public float hitColorTime = 0.3f;  //�ǰ� �� ���� �ð�    
    public bool isKnockbackEnable;     //�˹� �������� �÷���

    //�÷��̾� �������� �� ���� ����
    public GameObject player;
    public float chaseSpeed;



    private void Awake()
    {
        FSM = GetComponent<EnemyFSM>();
        reactionHandler = GetComponent<EnemyReactionHandler>();

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

}
