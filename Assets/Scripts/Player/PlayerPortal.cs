using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortal : MonoBehaviour
{
    //내 컴포넌트
    Player player;
    CapsuleCollider2D detectCollider;
    PlayerMove playerMove;

    //관련 변수들
    float PortalMoveTime = 0.5f;
    float PortalMoveTimer;
    float H;    //이동 방향에 따른 PlayerMove에 넘겨줄 값

    //포탈 무브 관련 변수
    bool Enter;                     //포탈 엔터 했을 때 플래그
    PortalWalkDirection WalkDir;    //포탈 무브 방향 설정

    //플래그
    bool isTargetScene;    //타겟 씬일 때 true

    private void Awake()
    {
        player = GetComponent<Player>();
        detectCollider = GetComponent<CapsuleCollider2D>();
        playerMove = GetComponent<PlayerMove>();
    }
    private void FixedUpdate()
    {
        //플레이어 포탈 진입 시 평범한 속도로 걸어가도록
        if (Enter)
        {
            PortalMoveTimer = PortalMoveTime;

            player.isPortalEnter = true;
            playerMove.Move(H);
        }
        Enter = false;

        if (player.isPortalEnter)
        {
            if (PortalMoveTimer > 0)
            {
                playerMove.Move(H);
                PortalMoveTimer -= Time.fixedDeltaTime;
            }
            else
            {
                PortalMoveTimer = 0;
                player.isPortalEnter = false;

                if (isTargetScene)
                {
                    isTargetScene = false;
                    //Context 변경 이벤트
                    InputEvents.InvokeContextUpdate(InputContext.SceneChange, false);
                }
            }
        }
    }

    private void OnEnable()
    {
        //Portal.cs에서 Portal 진입 시 이벤트 
        PortalEvents.OnPortalEnter += EnterPortal;
        //새로 진입한 씬에서 PlayerPosition값 새로 획득했을 때
        MapEvents.OnGetPlayerPos += TargetPortal;
    }
    private void OnDisable()
    {
        //Portal.cs에서 Portal 진입 시 이벤트 
        PortalEvents.OnPortalEnter -= EnterPortal;
        //새로 진입한 씬에서 PlayerPosition값 새로 획득했을 때
        MapEvents.OnGetPlayerPos -= TargetPortal;
    }
    //Portal.cs에서 Portal 진입 시 이벤트 
    void EnterPortal(string enterP, string targetS, string targetP, PortalWalkDirection walkDir)
    {
        //플래그 설정
        Enter = true;

        //도착 포탈일 때는 바로 리턴
        if (isTargetScene) return;        

        //출발 포탈일 때만 실행

        //감지 콜라이더, 히트박스 끄기
        detectCollider.enabled = false;
        player.playerHitBoxCollider.enabled = false;

        //카메라 Follow 리셋 이벤트 발행
        CameraEvents.InvokeCameraFollowReset();

        //WalkDir 갱신하도록 -> 출발과 도착 포탈 둘 다 같은 방향으로 움직이도록
        WalkDir = walkDir;
        SetWalkDir();

    }
    //새로 진입한 씬에서 PlayerPosition값 새로 획득했을 때
    void TargetPortal(Vector2 pos)
    {
        //플레이어 위치 초기화
        transform.position = pos;
        //감지 콜라이더, 히트박스 켜기
        detectCollider.enabled = true;
        player.playerHitBoxCollider.enabled = true;

        //플래그 설정
        isTargetScene = true;
    }

    //받은 WalkDir에 따라 플레이어가 움직일 방향 설정 
    void SetWalkDir()
    {
        switch (WalkDir)
        {
            case PortalWalkDirection.Left:
                H = -1f;
                break;

            case PortalWalkDirection.Right:
                H = 1f;
                break;

            case PortalWalkDirection.Up:

                break;

            case PortalWalkDirection.Down:

                break;
        }
    }
}
