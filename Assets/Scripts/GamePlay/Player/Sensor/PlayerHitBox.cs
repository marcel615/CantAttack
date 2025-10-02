using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour, IDamageable
{
    //IDamageable 인터페이스로 실행되는 메소드
    public void TakeDamage(Vector2 hitTargetPos, int damage)
    {
        PlayerEvents.InvokePlayerHitBoxHitted(hitTargetPos, damage);
    }
}
