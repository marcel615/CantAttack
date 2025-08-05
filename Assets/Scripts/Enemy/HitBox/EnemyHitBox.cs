using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour, IDamageable
{
    //부모 오브젝트
    [SerializeField] private EnemyBehavior enemyBehavior;

    private void Awake()
    {
        //오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (enemyBehavior == null) enemyBehavior = GetComponentInParent<EnemyBehavior>();
    }

    //IDamageable 인터페이스로 실행되는 메소드
    public void TakeDamage(Vector2 hitTargetPos, int damage)
    {
        EnemyEvents.InvokeEnemyHitBoxHitted(hitTargetPos, damage);

    }

}
