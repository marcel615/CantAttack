using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_A : EnemyBehavior
{
    //EnemyBehavior에서 EnemyFSM fsm 지정됨 fsm.ChangeEnemyState(EnemyState) 메서드를 통해서 State 변경하기


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
    public override void Dead()
    {
        Debug.Log("지금 사망");
    }

}
