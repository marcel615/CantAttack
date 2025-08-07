using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public EnemyController enemyController;


    //���� ���� ����
    [HideInInspector] public EnemyState currentState;

    //���� ���µ�
    public EnemyState idleState;
    public EnemyState patrolState;
    public EnemyState chaseState;
    public EnemyState attackState;
    public EnemyState stunState;
    public EnemyState evadeState;
    public EnemyState returnState;
    //public EnemyState hitState;
    public EnemyState deadState;




    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();

        idleState.Init(this);
        patrolState.Init(this);
        chaseState.Init(this);
        attackState.Init(this);
        stunState.Init(this);
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

    //���� ��ȯ ������ ���¸� true, ���� ��ȯ �Ұ����� ���¸� false ��ȯ
    public bool CanChangeState(EnemyState newState)
    {
        if (currentState == deadState)
        {
            return false;
        }
        else if (currentState == stunState)
        {
            if (newState == deadState) return true;
            else return false;
        }
        else if (currentState == attackState)
        {
            if (newState == deadState) return true;
            else if (newState == stunState) return true;
            else return false;
        }
        else if (currentState == chaseState)
        {
            return true;
        }
        else if (currentState == patrolState)
        {
            return true;
        }
        else if (currentState == idleState)
        {
            return true;
        }
        else //currentState�� null�� ��
        {
            return true;
        }
    }
    public EnemyState DecideNextState()
    {
        if (enemyController.isDead)
            return deadState;

        if (enemyController.isParryStun)
            return stunState;

        if (enemyController.isAttackEnable)
            return attackState;

        if (enemyController.isPlayerDetected)
            return chaseState;

        return idleState;
    }

}
