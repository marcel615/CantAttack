using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public EnemyController enemyController;
    public EnemyReactionHandler reactionHandler;

    //���� ���� ����
    public EnemyState currentState;

    //���� ���µ�
    public EnemyState idleState;
    public EnemyState chaseState;
    public EnemyState attackState;
    public EnemyState evadeState;
    public EnemyState returnState;
    //public EnemyState hitState;
    public EnemyState deadState;




    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        reactionHandler = GetComponent<EnemyReactionHandler>();

        idleState.Init(this);
        chaseState.Init(this);
        attackState.Init(this);
        evadeState.Init(this);
        returnState.Init(this);
        //hitState.Init(this);
        deadState.Init(this);

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
