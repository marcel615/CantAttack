using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSaw : MonoBehaviour
{
    Rigidbody2D rigid;
    CircleCollider2D circleCollider;


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
        if (collision.CompareTag("PlayerHitBox"))
        {
            Player player = collision.GetComponentInParent<Player>();
            if (player != null)
            {
                player.OnDamaged(transform.position);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {
            Player player = collision.GetComponentInParent<Player>();
            Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
            if (playerRigid != null)
            {
                playerRigid.WakeUp(); // 잠자기 모드 해제
            }

            if (player != null)
            {
                player.OnDamaged(transform.position);
            }
        }

    }

}
