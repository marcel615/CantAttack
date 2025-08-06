using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //내 컴포넌트
    public EnemyFSM FSM;
    public EnemyReactionHandler reactionHandler;

    //내 자식 오브젝트
    Transform groundCheckFront;
    Transform wallCheckFront;

    //발 밑에 땅이 있는지 체크 관련 변수들
    float checkRadius = 0.1f;
    LayerMask groundLayer;

    //Enemy 기본 정보들
    public int MaxHP = 10;
    public int CurrentHP;
    public float isHeadToRight = 1f; //캐릭터가 바라보는 방향(1이면 오른쪽, -1이면 왼쪽)

    //Idle에서 순찰할 때 관련 변수
    public float patrolSpeed;
    public float patrolMinDistance;
    public float patrolMaxDistance;
    public float patrolMinWaitTime;
    public float patrolMaxWaitTime;

    //피격되었을 때 관련 변수    
    public float hitColorTime = 0.3f;  //피격 색 변경 시간    
    public bool isKnockbackEnable;     //넉백 가능한지 플래그
    public float knockbackPower = 15f;
    public bool isKnockbacked;         //넉백 당했는지 플래그 (다른 움직임 잠깐 차단)
    public float knockbackCantMoveTime = 0.1f;  //넉백으로 잠시동안 움직임 차단 시간

    //플레이어 감지했을 때 관련 변수
    public GameObject player;
    public float chaseSpeed;



    private void Awake()
    {
        FSM = GetComponent<EnemyFSM>();
        reactionHandler = GetComponent<EnemyReactionHandler>();

        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        //앞에 땅이 있는지 체크하는 오브젝트 연결
        if (groundCheckFront == null) groundCheckFront = transform.Find("GroundCheckFront")?.GetComponent<Transform>();
        //앞에 막혀있는지 체크하는 오브젝트 연결
        if (wallCheckFront == null) wallCheckFront = transform.Find("WallCheckFront")?.GetComponent<Transform>();

        //땅 체크 변수
        groundLayer = LayerMask.GetMask("Ground", "");

        CurrentHP = MaxHP;
        isKnockbackEnable = true;
    }

    //이벤트 구독
    private void OnEnable()
    {
        //EnemyHitBox에서 Hit되었을 때
        EnemyEvents.OnEnemyHitBoxHitted_EnemyDamageHandler += OnDamaged;
        //EnemyPlayerDetector에서 플레이어를 감지했을 때
        EnemyEvents.OnEnemyPlayerDetected += OnPlayerDetect;
        //EnemyChaseRange에서 플레이어가 감지에서 Exit했을 때
        EnemyEvents.OnEnemyChaseOver += OnEnemyChaseOver;
    }
    private void OnDisable()
    {
        //EnemyHitBox에서 Hit되었을 때
        EnemyEvents.OnEnemyHitBoxHitted_EnemyDamageHandler -= OnDamaged;
        //EnemyPlayerDetector에서 플레이어를 감지했을 때
        EnemyEvents.OnEnemyPlayerDetected -= OnPlayerDetect;
        //EnemyChaseRange에서 플레이어가 감지에서 Exit했을 때
        EnemyEvents.OnEnemyChaseOver -= OnEnemyChaseOver;
    }
    //피격 이벤트 발생 시
    void OnDamaged(Vector2 hitTargetPos, int damage)
    {
        //데미지 적용시키기
        CurrentHP -= damage;

        //남은 체력에 따라 넉백 혹은 FSM State 전환
        if (CurrentHP > 0)
        {
            if (isKnockbackEnable)
            {
                reactionHandler.HitWithKnockback(hitColorTime, hitTargetPos);
            }
            else
            {
                reactionHandler.HitWithoutKnockback(hitColorTime);
            }
        }
        else
        {
            FSM.ChangeState(FSM.deadState);
        }
    }

    void OnPlayerDetect(GameObject P)
    {
        player = P;
        FSM.ChangeState(FSM.chaseState);
    }
    void OnEnemyChaseOver(GameObject P)
    {
        player = null;
        FSM.ChangeState(FSM.idleState);
    }

    //앞에 땅이 있는지 체크하는 메서드 (땅이 있으면 true, 없으면 false 반환)
    public bool isGroundFront()
    {
        return Physics2D.Raycast(groundCheckFront.position, Vector2.down, checkRadius, groundLayer);
    }
    //앞에 벽이 있는지 체크하는 메서드 (벽이 있으면 true, 없으면 false 반환)
    public bool isWallFront()
    {
        Vector2 checkDir = new Vector2(isHeadToRight, 0);
        return Physics2D.Raycast(wallCheckFront.position, checkDir, checkRadius, groundLayer);
    }

}
