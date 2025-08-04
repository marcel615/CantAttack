using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCrusherBlockDamagerCollider : MonoBehaviour
{
    [SerializeField] private TrapCrusherBlock crusherBlock;

    //�÷��̾� �� ������� �浹 �� ���� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(transform.position, crusherBlock.damage);
        }
    }
}
