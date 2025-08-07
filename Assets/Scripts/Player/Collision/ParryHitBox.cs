using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHitBox : MonoBehaviour
{
    [SerializeField] private Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IParryable>(out IParryable parriedTarget))
        {
            parriedTarget.OnParried(player.gameObject);
            PlayerEvents.InvokePlayerParrySuccess();    //패리 성공했다는 이벤트 발행
        }

    }
}
