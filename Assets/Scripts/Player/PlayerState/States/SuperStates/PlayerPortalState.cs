using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortalState : PlayerState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;
    CapsuleCollider2D playerCollider;

    //portal 관련 Controller 변수
    PortalWalkDirection walkDir;
    float portalMoveTime;
    float normalSpeed;
    float normalJumpPower;

    //portal 관련 변수
    float portalMoveTimer;
    float H;
    bool isPortalEnterStart;
    bool isTargetScene;
    bool isCanChange;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }
    public override void Enter()
    {
        portalMoveTime = FSM.playerController.portalMoveTime;
        normalSpeed = FSM.playerController.normalSpeed;
        normalJumpPower = FSM.playerController.normalJumpPower;

        animator.SetTrigger("isIdle");
        EnterPortal();
        portalMoveTimer = portalMoveTime;

        isCanChange = false;
    }
    public override void UpdateState()
    {
    }
    public override void FixedUpdateState()
    {
        if (!isPortalEnterStart) return;

        if (portalMoveTimer > 0)
        {
            //포탈에서 위로 올라가는 경우 말고는 여기서
            if (walkDir != PortalWalkDirection.Up)
            {
                rigid.velocity = new Vector2(H * normalSpeed, rigid.velocity.y);
                if (H != 0)
                {
                    //H에 따라 캐릭터 좌우 반전
                    //transform.localScale = new Vector3(H, 1, 1);
                    SetLocalScale(H);

                    FSM.playerController.isHeadToRight = (H > 0) ? 1 : -1; //H가 양수면 1 저장, 음수면 -1 저장
                    animator.SetBool("isMoving", true);
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }
            }
            else  //포탈에서 위로 올라가는 경우는 여기서 처리
            {
                rigid.velocity = new Vector2(H * normalSpeed, normalJumpPower);
            }
            portalMoveTimer -= Time.fixedDeltaTime;
        }
        else
        {
            portalMoveTimer = 0;
            isPortalEnterStart = false;

            if (isTargetScene)
            {
                isTargetScene = false;
                //Context 변경 이벤트
                InputEvents.InvokeContextUpdate(InputContext.Player);

                isCanChange = true;
            }
        }

    }
    public override void Exit()
    {
    }
    public override void SetChangeState()
    {
        allowedTransitions.Add(FSM.groundState);
        allowedTransitions.Add(FSM.inAirState);
    }
    public override bool CanChangeState(PlayerState newState)
    {
        if (newState == FSM.portalState)
            return true;
        return (isCanChange && base.CanChangeState(newState));
    }
    private void OnEnable()
    {
        //새로 진입한 씬에서 PlayerPosition값 새로 획득했을 때
        MapEvents.OnGetPlayerPos += TargetScene;
    }
    private void OnDisable()
    {
        //새로 진입한 씬에서 PlayerPosition값 새로 획득했을 때
        MapEvents.OnGetPlayerPos -= TargetScene;
    }
    void EnterPortal()
    {
        isPortalEnterStart = true;
        //도착 포탈일 때는 바로 리턴
        if (isTargetScene) return;

        //출발 포탈일 때만 실행

        //감지 콜라이더, 히트박스 끄기
        playerCollider.enabled = false;
        FSM.playerController.playerHitBoxCollider.enabled = false;

        //카메라 Follow 리셋 이벤트 발행
        CameraEvents.InvokeCameraFollowReset();

        //WalkDir 갱신하도록 -> 출발과 도착 포탈 둘 다 같은 방향으로 움직이도록
        walkDir = FSM.playerController.walkDir;
        SetWalkDir();
    }
    //받은 WalkDir에 따라 플레이어가 움직일 방향 설정 
    void SetWalkDir()
    {
        switch (walkDir)
        {
            case PortalWalkDirection.Left:
                H = -1f;
                break;

            case PortalWalkDirection.Right:
                H = 1f;
                break;

            case PortalWalkDirection.Up:
                H = 0;
                break;

            case PortalWalkDirection.Down:
                H = 0;
                break;
        }
    }
    void TargetScene(Vector2 pos)
    {
        //플래그 설정
        isTargetScene = true;
        //플레이어 위치 초기화
        transform.position = pos;
        //감지 콜라이더, 히트박스 켜기
        playerCollider.enabled = true;
        FSM.playerController.playerHitBoxCollider.enabled = true;


        //포탈에서 위로 올라가는 경우에는 타겟씬 포탈은 타지 않기 때문에 여기서 플래그 초기화 및 Context 전환
        if (walkDir == PortalWalkDirection.Up)
        {
            isTargetScene = false;

            rigid.velocity = new Vector2(0, 0);
            animator.SetTrigger("isIdle");

            //Context 변경 이벤트
            InputEvents.InvokeContextUpdate(InputContext.Player);

            isCanChange = true;
        }
    }
    void SetLocalScale(float dir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir;
        transform.localScale = scale;
    }

}
