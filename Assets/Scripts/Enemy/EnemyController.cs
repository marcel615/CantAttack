using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //내 컴포넌트
    public EnemyFSM FSM;
    public EnemyReactionHandler reactionHandler;

    //Enemy 기본 정보들
    public int MaxHP = 10;
    public int CurrentHP;

    //Idle에서 순찰할 때 관련 변수
    public float patrolSpeed;
    public float patrolMinDistance;
    public float patrolMaxDistance;
    public float patrolMinWaitTime;
    public float patrolMaxWaitTime;

    //피격되었을 때 관련 변수    
    public float hitColorTime = 0.3f;  //피격 색 변경 시간    
    public bool isKnockbackEnable;     //넉백 가능한지 플래그

    //플레이어 감지했을 때 관련 변수
    public GameObject player;
    public float chaseSpeed;



    private void Awake()
    {
        FSM = GetComponent<EnemyFSM>();
        reactionHandler = GetComponent<EnemyReactionHandler>();

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

}
