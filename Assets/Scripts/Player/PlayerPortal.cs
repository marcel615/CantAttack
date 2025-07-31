using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortal : MonoBehaviour
{
    Player player;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    //관련 변수들
    float PortalMoveTime = 0.5f;
    float PortalMoveTimer;
    float MoveSpeed = 6f;

    //포탈 무브 관련 변수
    bool Enter;                     //포탈 엔터 했을 때 플래그
    PortalWalkDirection WalkDir;    //포탈 무브 방향 설정

    //플래그
    bool isEnterPortal;    //포탈에 들어가면 true로 바뀜
    bool isTargetScene;    //씬 이동 끝나면 true로 바뀜

    private void Awake()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        //플레이어 포탈 진입 시 평범한 속도로 걸어가도록
        if (Enter)
        {
            PortalMoveTimer = PortalMoveTime;

            player.isPortalEnter = true;
            player.InvincibleTimer = PortalMoveTime;
            player.isInvincible = true;

            rigid.velocity = new Vector2(player.isHeadToRight * MoveSpeed, 0);
        }
        Enter = false;

        if (player.isPortalEnter)
        {
            if (PortalMoveTimer > 0)
            {
                rigid.velocity = new Vector2(player.isHeadToRight * MoveSpeed, 0);
                PortalMoveTimer -= Time.fixedDeltaTime;
            }
            else
            {
                PortalMoveTimer = 0;
                player.isPortalEnter = false;

                //출발포탈에서의 무빙 끝났으니 Scene 변경하기
                if (isEnterPortal && !isTargetScene)
                {
                    //(포탈을 통한) Map을 바꾸라는 요청 이벤트 발행
                    MapEvents.InvokeRequestMapMove_Portal();
                    isEnterPortal = false;
                }
                //도착포탈에서의 무빙 끝났으니 InputContext를 Player로 설정하기
                else if(isEnterPortal && isTargetScene)
                {
                    //Context 변경 이벤트
                    InputEvents.InvokeContextUpdate(InputContext.SceneChange, false);
                    //타겟 씬으로 이동한 상태에서 PlayerPortalMove가 종료했을 때
                    PlayerEvents.InvokePlayerPortalMoveOver();
                    isEnterPortal = false;
                    isTargetScene = false;
                }
            }

        }
    }

    private void OnEnable()
    {
        PortalEvents.OnPortalEnter += EnterPortal;
        MapEvents.OnGetPlayerPos += TargetPortal;


    }
    private void OnDisable()
    {
        PortalEvents.OnPortalEnter -= EnterPortal;
        MapEvents.OnGetPlayerPos -= TargetPortal;

    }
    void EnterPortal(string enterP, string targetS, string targetP, PortalWalkDirection walkDir)
    {
        //출발 포탈에서만 WalkDir 갱신하도록 -> 출발과 도착 포탈 둘 다 같은 방향으로 움직이도록
        if (!isTargetScene)
        {
            WalkDir = walkDir;
            SetWalkDir();
        }
        Enter = true;
        isEnterPortal = true;
    }
    void TargetPortal(Vector2 pos)
    {
        isTargetScene = true;
    }
    //받은 WalkDir에 따라 플레이어가 움직일 방향 설정 
    void SetWalkDir()
    {
        switch (WalkDir)
        {
            case PortalWalkDirection.Left:
                player.isHeadToRight = -1f;
                spriteRenderer.flipX = true;
                break;

            case PortalWalkDirection.Right:
                player.isHeadToRight = 1f;
                spriteRenderer.flipX = false;
                break;

            case PortalWalkDirection.Up:

                break;

            case PortalWalkDirection.Down:

                break;
        }
    }
}
