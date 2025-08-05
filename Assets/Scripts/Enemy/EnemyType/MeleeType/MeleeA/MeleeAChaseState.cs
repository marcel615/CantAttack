using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAChaseState : EnemyState
{
    //내 컴포넌트
    Rigidbody2D rigid;

    //Chase 관련 Controller 변수
    GameObject player;
    float chaseSpeed;

    //Chase 관련 변수
    int chaseDir;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public override void Enter()
    {
        //Controller 변수 초기화
        player = FSM.enemyController.player;
        chaseSpeed = FSM.enemyController.chaseSpeed;
    }

    public override void UpdateState()
    {
        //chase 방향 설정 후 chase 방향으로 chaseSpeed로 움직이기
        chaseDir = (player.transform.position.x > transform.position.x) ? 1 : -1;
        rigid.velocity = new Vector2(chaseDir * chaseSpeed, rigid.velocity.y);
    }

    public override void Exit()
    {
        
    }
}
