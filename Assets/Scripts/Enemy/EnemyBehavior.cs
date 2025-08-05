using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    protected EnemyFSM fsm;
    public EnemyReactionHandler reactionHandler;

    protected bool isKnockbackEnable;
    public int MaxHP;
    public int CurrentHP;

    public void Init()
    {
        fsm = GetComponent<EnemyFSM>();
        reactionHandler = GetComponent<EnemyReactionHandler>();
    }    
    public abstract void Idle();
    public abstract void DetectAndChasePlayer();
    public abstract void Attack();
    public abstract void Evade();
    public abstract void Return();
    public abstract void Hit(Vector2 hittedPos);
    public abstract void Dead();

}
