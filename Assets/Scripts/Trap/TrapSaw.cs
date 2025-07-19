using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSaw : MonoBehaviour
{
    //내 컴포넌트
    Rigidbody2D rigid;
    CircleCollider2D circleCollider;

    //기본 변수
    int damage = 1;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    //플레이어 및 적들과의 충돌 시 로직 구현
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(transform.position, damage);
        }
    }


}
