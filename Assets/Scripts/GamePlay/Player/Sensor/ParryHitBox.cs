using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IParryable>(out var parriedTarget))
        {
            parriedTarget.OnParried(gameObject);
            PlayerEvents.InvokePlayerParrySuccess();    //패리 성공했다는 이벤트 발행

            //패리 가능한 투사체
            if (collision.TryGetComponent<ProjectileBase>(out var parriedProjectile))
            {
                PlayerEvents.InvokeProjectileParried(parriedProjectile, parriedProjectile.Sender);
            }
            //패리 가능한 근접 공격
            else
            {

            }
        }
    }
}
