using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAChaseState : EnemyState
{
    //�� ������Ʈ
    Rigidbody2D rigid;

    //Chase ���� Controller ����
    GameObject player;
    float chaseSpeed;

    //Chase ���� ����
    int chaseDir;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public override void Enter()
    {
        //Controller ���� �ʱ�ȭ
        player = FSM.enemyController.player;
        chaseSpeed = FSM.enemyController.chaseSpeed;
    }

    public override void UpdateState()
    {
        //chase ���� ���� �� chase �������� chaseSpeed�� �����̱�
        chaseDir = (player.transform.position.x > transform.position.x) ? 1 : -1;
        rigid.velocity = new Vector2(chaseDir * chaseSpeed, rigid.velocity.y);
    }

    public override void Exit()
    {
        
    }
}
