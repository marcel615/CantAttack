using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_A : EnemyBehavior
{
    //EnemyBehavior���� EnemyFSM fsm ������ fsm.ChangeEnemyState(EnemyState) �޼��带 ���ؼ� State �����ϱ�


    public override void Idle()
    {
        Debug.Log("���� Idle��");        
    }
    public override void DetectAndChasePlayer()
    {
        Debug.Log("���� Player ������");
    }
    public override void Attack()
    {
        Debug.Log("���� ������");
    }  
    public override void Evade()
    {
        Debug.Log("���� ȸ����");
    }
    public override void Return()
    {
        Debug.Log("���� ���� ��ġ�� ���ư��� ��");
    }
    public override void Dead()
    {
        Debug.Log("���� ���");
    }

}
