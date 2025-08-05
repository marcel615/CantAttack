using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_A : EnemyBehavior
{
    /// <EnemyBehavior�Ҵ纯��>
    /// EnemyFSM fsm
    /// EnemyReactionHandler reactionHandler
    /// </EnemyBehavior�Ҵ纯��>    

    private void Awake()
    {
        //EnemyBehavior�� Init() ���� -> ������Ʈ�� �Ҵ�
        base.Init();

        //EnemyBehavior�� ���Ҵ� ������ �ʱ�ȭ
        isKnockbackEnable = true;
        MaxHP = 5;
        CurrentHP = 5;
    }

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
        Debug.Log("���� ���");
    }

}
