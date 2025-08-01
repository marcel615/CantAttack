using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortal : MonoBehaviour
{
    //내 컴포넌트
    Player player;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D detectCollider;

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
        detectCollider = GetComponent<CapsuleCollider2D>();
    }
    private void FixedUpdate()
    {
        //플레이어 포탈 진입 시 평범한 속도로 걸어가도록
        if (Enter)
        {
            PortalMoveTimer = PortalMoveTime;

            player.isPortalEnter = true;

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
                    isEnterPortal = false;
                }
                //도착포탈에서의 무빙 끝났으니 InputContext를 Player로 설정하기
                else if(isEnterPortal && isTargetScene)
                {
                    isEnterPortal = false;
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
        //출발 포탈에서만 실행
        if (!isTargetScene)
        {
            //감지 콜라이더, 히트박스 끄기
            detectCollider.enabled = false;
            player.playerHitBoxCollider.enabled = false;

            //카메라 Follow 리셋 이벤트 발행
            CameraEvents.InvokeCameraFollowReset();

            //WalkDir 갱신하도록 -> 출발과 도착 포탈 둘 다 같은 방향으로 움직이도록
            WalkDir = walkDir;
            SetWalkDir();
        }
        //플래그 설정
        Enter = true;
        isEnterPortal = true;
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
