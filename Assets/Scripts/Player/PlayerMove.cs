using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //내 컴포넌트
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Player player;

    //Move 관련 입력 변수
    float H;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        //플레이어 기본 움직임 설정
        if (player.canControl)
        {
            rigid.velocity = new Vector2(H * player.normalSpeed, rigid.velocity.y);
        }
        //H값에 따른 플레이어 캐릭터가 바라보는 방향 및 애니메이션 설정
        if (H != 0 && player.canControl)
        {
            spriteRenderer.flipX = H < 0;
            animator.SetBool("isMoving", true);
            player.isHeadToRight = (H > 0) ? 1 : -1; //H가 양수면 1 저장, 음수면 -1 저장
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        //Move를 더이상 받지 않는데도 계속 움직이는 상황 해결하기 위해 H = 0으로 초기화시켜주기
        H = 0;

    }

    public void Move(float h)
    {
        H = h;
    }

}
