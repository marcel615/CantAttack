using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCrusherBlockDamagerCollider : MonoBehaviour
{
    //�� ������Ʈ
    BoxCollider2D bodyCollider;

    //�⺻ ����
    [SerializeField] private int damage = 1;

    private void Awake()
    {
        bodyCollider = GetComponent<BoxCollider2D>();
    }

    //�÷��̾� �� ������� �浹 �� ���� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(transform.position, damage);
        }
    }


}
