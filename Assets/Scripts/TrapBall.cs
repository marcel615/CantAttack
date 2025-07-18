using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrapBall : MonoBehaviour
{
    //내 컴포넌트
    Rigidbody2D rigid;

    //발사 변수
    GameObject Target;  //발사 대상
    Vector2 direction;  //발사 방향
    float speed = 12f;   //발사 속도
    float vanishTime = 3f; //존재 시간

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rigid.velocity = direction * speed;
        Destroy(gameObject, vanishTime);

    }

    void Update()
    {
        if(Target == null)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.OnDamaged(transform.position);
                Destroy(gameObject);
            }

        }
    }

    //타겟정보 가져오기
    public void SetTarget(GameObject target)
    {
        Target = target;
        SetDirection();
    }
    //발사 방향 정하기
    void SetDirection()
    {
        direction = (Target.transform.position - transform.position).normalized; //방향 normalize하기

    }
}
