using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour, IDamageable
{
    //부모 오브젝트
    [SerializeField] private Player player;

    private void Awake()
    {
        //오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
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
