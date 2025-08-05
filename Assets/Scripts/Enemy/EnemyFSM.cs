using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public EnemyController enemyController;
    public EnemyReactionHandler reactionHandler;

    //상태 추적 변수
    public EnemyState currentState;

    //여러 상태들
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

}
