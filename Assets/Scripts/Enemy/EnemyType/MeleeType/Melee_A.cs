using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_A : EnemyBehavior
{
    /// <EnemyBehavior할당변수>
    /// EnemyFSM fsm
    /// EnemyReactionHandler reactionHandler
    /// </EnemyBehavior할당변수>    

    private void Awake()
    {
        //EnemyBehavior의 Init() 실행 -> 컴포넌트들 할당
        base.Init();

        //EnemyBehavior의 미할당 변수들 초기화
        isKnockbackEnable = true;
        MaxHP = 5;
        CurrentHP = 5;
    }

    public override void Idle()
    {
        Debug.Log("지금 Idle중");        
    }
    public override void DetectAndChasePlayer()
    {
        Debug.Log("지금 Player 추적중");
    }
    public override void Attack()
    {
        Debug.Log("지금 공격중");
    }  
    public override void Evade()
    {
        Debug.Log("지금 회피중");
    }
    public override void Return()
    {
        Debug.Log("지금 원래 위치로 돌아가는 중");
    }
    public override void Hit(Vector2 hittedPos)
    {
        if (isKnockbackEnable)
        {
            reactionHandler.HitWithKnockback(hittedPos);
        }
        else
        {
            reactionHandler.HitWithNoKnockback();
        }
    }
    public override void Dead()
    {
        Debug.Log("지금 사망");
    }

}
