using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    //내 컴포넌트
    public PlayerController playerController;

    //여러 상태들
    //SuperState
    public PlayerState groundState;
    public PlayerState inAirState;
    public PlayerState dashState;
    public PlayerState parryState;
    public PlayerState hitState;
    public PlayerState deadState;
    public PlayerState spawnState;
    public PlayerState portalState;
    //SubState
    public PlayerState idleState;
    public PlayerState moveState;
    public PlayerState jumpState;
    public PlayerState fallState;

    //상태 추적 변수
    [HideInInspector] public PlayerState currentState;



    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        groundState.Init(this);
        inAirState.Init(this);
        dashState.Init(this);
        parryState.Init(this);
        hitState.Init(this);
        deadState.Init(this);
        spawnState.Init(this);
        portalState.Init(this);

        idleState.Init(this);
        moveState.Init(this);
        jumpState.Init(this);
        fallState.Init(this);
    }
    private void Start()
    {
        //첫 번째 State로 idleState 지정
        ChangeState(groundState);
    }
    private void Update()
    {
        //Debug.Log(currentState);
        //currentState가 존재하면 UpdateState() 계속 실행시키기
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }
    private void FixedUpdate()
    {
        //currentState가 존재하면 FixedUpdateState() 계속 실행시키기
        if (currentState != null)
        {
            currentState.FixedUpdateState();
        }
    }

    //State 변화가 들어오면 여기서 처리
    public void ChangeState(PlayerState newState)
    {
        //이전 State의 Exit() 메서드 실행시키고 
        if (currentState != null)
        {
            currentState.Exit();
        }
        //새로 들어온 State를 현재 State로 설정 후 Enter() 메서드 실행시키기
        currentState = newState;
        currentState.Enter();
    }

    /*
    public PlayerState DecideNextState()
    {
        if (playerController.isDead)
            return deadState;

        if (playerController.isParryStun)
            return stunState;

        if (playerController.isAttackEnable)
            return attackState;

        if (playerController.isPlayerDetected)
            return chaseState;

        return idleState;
    }
    */
}
