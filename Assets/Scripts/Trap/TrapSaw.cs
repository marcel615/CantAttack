using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSaw : MonoBehaviour
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    CircleCollider2D circleCollider;

    //�⺻ ����
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

    //�÷��̾� �� ������� �浹 �� ���� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(transform.position, damage);
        }
    }


}
