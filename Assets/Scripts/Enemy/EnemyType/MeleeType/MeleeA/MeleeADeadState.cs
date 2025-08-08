using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeADeadState : EnemyState
{
    //내 컴포넌트
    SpriteRenderer spriteRenderer; 
    Animator animator;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public override void Enter()
    {
        //콜라이더 끄기
        FSM.enemyController.DeadDisableColliders();
        //Dead 시 반투명하게 됨
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //Dead 애니메이션 실행
        animator.SetBool("isDead", true);
    }
    public override void UpdateState()
    {
    }
    public override void Exit()
    {
    }
}
