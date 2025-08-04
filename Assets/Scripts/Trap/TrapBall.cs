using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrapBall : MonoBehaviour, IParryable
{
    //내 컴포넌트
    Rigidbody2D rigid;

    //발사 변수
    GameObject Target;  //발사 대상
    GameObject Sender;  //발사 요청한 곳
    Vector2 direction;  //발사 방향
    float speed = 12f;   //발사 속도
    float vanishTime = 3f; //존재 시간

    //Destroy되는 시간 코루틴
    Coroutine vanishCoroutine;

    //기본 변수
    [SerializeField] private int damage = 1;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Sender) return;

        if (collision.TryGetComponent<IDamageable>(out IDamageable target))
        {
            Debug.Log("Test");
            target.TakeDamage(transform.position, damage);
            Destroy(gameObject);
        }
    }

    //타겟정보 설정하기
    public void SetTarget(GameObject target, GameObject sender)
    {
        Target = target;
        Sender = sender;
        SetDirection();
        Shoot();
    }
    //패링 인터페이스 구현
    public void OnParried(GameObject parryOrigin)
    {
        CancelDestroy();
        SetTarget(Sender, parryOrigin);
    }
    //발사 방향 정하기
    void SetDirection()
    {
        direction = (Target.transform.position - transform.position).normalized; //방향 normalize하기
    }
    //발사하기
    void Shoot()
    {
        rigid.velocity = direction * speed;
        vanishCoroutine = StartCoroutine(VanishAfterTime());
    }

    //Destroy 타이머 설정
    private IEnumerator VanishAfterTime()
    {
        yield return new WaitForSeconds(vanishTime);
        Destroy(gameObject);
    }
    void CancelDestroy()
    {
        // 기존 타이머 취소
        if (vanishCoroutine != null)
        {
            StopCoroutine(vanishCoroutine);
        }
    }


}
