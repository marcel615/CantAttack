using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    //FSM
    protected PlayerFSM FSM;

    //전환 가능 State들 모음
    protected List<PlayerState> allowedTransitions = new List<PlayerState>();

    //전환 가능한 State이면 true, 아니면 false 반환
    public virtual bool CanChangeState(PlayerState newState)
    {
        return allowedTransitions.Contains(newState);
    }

    public virtual void Init(PlayerFSM fsm)
    {
        FSM = fsm;
        SetChangeState();
    }
    public abstract void Enter();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void Exit();
    public abstract void SetChangeState();


}
