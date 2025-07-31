using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortal : MonoBehaviour
{
    Player player;
    Rigidbody2D rigid;

    //관련 변수들
    float PortalMoveTime = 0.5f;
    float PortalMoveTimer;
    float MoveSpeed = 6f;

    //엔터 관련 플래그
    bool Enter;  //포탈 엔터 했을 때 플래그

    //플래그
    bool isEnterPortal;    //포탈에 들어가면 true로 바뀜
    bool isTargetScene;    //씬 이동 끝나면 true로 바뀜

    private void Awake()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody2D>();
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
    void EnterPortal(string enterP, string targetS, string targetP)
    {
        Enter = true;
        isEnterPortal = true;
    }
    void TargetPortal(Vector2 pos)
    {
        isTargetScene = true;
    }
}
