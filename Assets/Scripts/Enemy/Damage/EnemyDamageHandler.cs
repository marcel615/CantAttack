using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    //내 컴포넌트
    protected EnemyBehavior enemyBehavior;

    private void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
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
        enemyBehavior.CurrentHP -= damage;

        //남은 체력에 따라 Hit() 혹은 Dead() 실행시키기
        if (enemyBehavior.CurrentHP > 0)
            enemyBehavior.Hit(hitTargetPos);    
        else        
            enemyBehavior.Dead();
    }  

}
