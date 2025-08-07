using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public EnemyController enemyController;


    //상태 추적 변수
    [HideInInspector] public EnemyState currentState;

    //여러 상태들
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
        //첫 번째 State로 idleState 지정
        ChangeState(idleState);
    }
    private void Update()
    {
        //currentState가 존재하면 UpdateState() 계속 실행시키기
        if(currentState != null)
        {
            currentState.UpdateState();
        }
    }

    //State 변화가 들어오면 여기서 처리
    public void ChangeState(EnemyState newState)
    {
        //이전 State의 Exit() 메서드 실행시키고 
        if(currentState != null)
        {
            currentState.Exit();
        }
        //새로 들어온 State를 현재 State로 설정 후 Enter() 메서드 실행시키기
        currentState = newState;
        currentState.Enter();
    }

    //상태 전환 가능한 상태면 true, 상태 전환 불가능한 상태면 false 반환
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
        else //currentState가 null일 때
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
