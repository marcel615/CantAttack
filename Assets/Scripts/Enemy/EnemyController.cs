using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //내 컴포넌트
    EnemyFSM FSM;

    //인스펙터 할당 컴포넌트
    public EnemyState hitState;
    public EnemyState deadState;

    public int MaxHP;
    public int CurrentHP;
    public float patrolSpeed;

    public void Init()
    {
        FSM = GetComponent<EnemyFSM>();
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

        //남은 체력에 따라 FSM에 State 전달
        if (CurrentHP > 0)
            FSM.OnHit(hitTargetPos);
        else
            FSM.OnDead();
    }

}
