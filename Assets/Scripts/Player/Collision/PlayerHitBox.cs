using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour, IDamageable
{
    //부모 오브젝트
    public Player player;

    private void Awake()
    {
        //오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (player == null) player = GetComponentInParent<Player>();
    }

    //IDamageable 인터페이스로 실행되는 메소드
    public void TakeDamage(Vector2 hitTargetPos, int damage)
    {
        PlayerEvents.InvokePlayerHitBoxHitted(hitTargetPos, damage);
    }
}
