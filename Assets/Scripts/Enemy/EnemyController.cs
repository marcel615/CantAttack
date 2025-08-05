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
    public float patrolSpeed;
    public float chaseSpeed;

    //Enemy 기본 플래그
    public bool isKnockbackEnable;



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
        EnemyEvents.OnEnemyHitBoxHitted_EnemyDamageHandler += OnDamaged;
    }
    private void OnDisable()
    {
        EnemyEvents.OnEnemyHitBoxHitted_EnemyDamageHandler -= OnDamaged;
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
                reactionHandler.HitWithKnockback(hitTargetPos);
            }
            else
            {
                reactionHandler.HitWithoutKnockback();
            }
        }
        else
        {
            FSM.ChangeState(FSM.deadState);
        }
    }

}
