using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    protected EnemyFSM FSM;
    public virtual void Init(EnemyFSM fsm)
    {
        FSM = fsm;
    }
    public abstract void Enter();
    public abstract void UpdateState();
    public abstract void Exit();
}
