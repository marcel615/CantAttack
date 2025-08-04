using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    protected EnemyFSM fsm;
    public void Init(EnemyFSM fsm)
    {
        this.fsm = fsm;
    }    
    public abstract void Idle();
    public abstract void DetectAndChasePlayer();
    public abstract void Attack();
    public abstract void Evade();
    public abstract void Return();
    public abstract void Dead();

}
