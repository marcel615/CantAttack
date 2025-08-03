using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{
    //내 컴포넌트
    BoxCollider2D damageCollider;

    //기본 변수
    [SerializeField] private int damage = 1;


    private void Awake()
    {
        damageCollider = GetComponent<BoxCollider2D>();
    }

    //플레이어 및 적들과의 충돌 시 로직 구현
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(transform.position, damage);
        }
    }

}
