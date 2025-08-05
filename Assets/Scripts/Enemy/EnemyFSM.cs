using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    //���� ���� ����
    private EnemyState currentState;

    //���� ���µ�
    public EnemyState idleState;
    public EnemyState chaseState;
    public EnemyState attackState;
    public EnemyState evadeState;
    public EnemyState returnState;
    //public EnemyState hitState;
    public EnemyState deadState;

    EnemyReactionHandler reactionHandler;



    private void Awake()
    {
        idleState.Init(this);
        chaseState.Init(this);
        attackState.Init(this);
        evadeState.Init(this);
        returnState.Init(this);
        //hitState.Init(this);
        deadState.Init(this);

        reactionHandler = GetComponent<EnemyReactionHandler>();
    }
    private void Start()
    {
        //ù ��° State�� idleState ����
        ChangeState(idleState);
    }
    private void Update()
    {
        //currentState�� �����ϸ� UpdateState() ��� �����Ű��
        if(currentState != null)
        {
            currentState.UpdateState();
        }
    }

    public void OnHit(Vector2 hitTargetPos)
    {
        if (currentState == (chaseState || attackState))
        {
            reactionHandler.HitWithNoKnockback();
        }
        else
        {
            reactionHandler.HitWithKnockback(hitTargetPos);
        }
    }
    public void OnDead()
    {
        ChangeState(deadState);
    }

    //State ��ȭ�� ������ ���⼭ ó��
    public void ChangeState(EnemyState newState)
    {
        //���� State�� Exit() �޼��� �����Ű�� 
        if(currentState != null)
        {
            currentState.Exit();
        }
        //���� ���� State�� ���� State�� ���� �� Enter() �޼��� �����Ű��
        currentState = newState;
        currentState.Enter();
    }

}
