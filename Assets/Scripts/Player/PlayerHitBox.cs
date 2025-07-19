using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour, IDamageable
{
    //�θ� ������Ʈ
    [SerializeField] private Player player;

    private void Awake()
    {
        //������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (player == null) player = GetComponentInParent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(Vector2 hitPosition, int damage)
    {
        player.OnDamaged(hitPosition, damage);

    }
}
