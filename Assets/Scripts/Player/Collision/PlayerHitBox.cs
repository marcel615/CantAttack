using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour, IDamageable
{
    //�θ� ������Ʈ
    public Player player;

    private void Awake()
    {
        //������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (player == null) player = GetComponentInParent<Player>();
    }

    //IDamageable �������̽��� ����Ǵ� �޼ҵ�
    public void TakeDamage(Vector2 hitTargetPos, int damage)
    {
        PlayerEvents.InvokePlayerHitBoxHitted(hitTargetPos, damage);
    }
}
