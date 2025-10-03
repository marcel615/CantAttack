using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    //내 컴포넌트
    protected Rigidbody2D rigid;

    //발사 관련 변수
    protected GameObject Target;
    public GameObject Sender;
    protected Vector2 Direction;

    //개별 설정
    [SerializeField] public float speed;
    [SerializeField] public float vanishTime;
    [SerializeField] public int damage;

    //Destroy되는 시간 코루틴
    protected Coroutine vanishCoroutine;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Sender) return;

        if (collision.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(transform.position, damage);
            Destroy(gameObject);
        }
    }
    //타겟정보 설정하기
    public virtual void SetTarget(GameObject target, GameObject sender)
    {
        Target = target;
        Sender = sender;

        Direction = (Target.transform.position - transform.position).normalized;
        Shoot();
    }

    //발사 방향 설정하기
    public virtual void SetDirection(Vector2 dir, GameObject sender)
    {
        Sender = sender;

        Direction = dir.normalized;
        Shoot();
    }

    //발사하기
    protected virtual void Shoot()
    {
        rigid.velocity = Direction * speed;
        vanishCoroutine = StartCoroutine(VanishAfterTime());
    }

    //Destroy 타이머 설정
    protected IEnumerator VanishAfterTime()
    {
        yield return new WaitForSeconds(vanishTime);
        Destroy(gameObject);
    }

    // 기존 타이머 취소
    protected void CancelDestroy()
    {
        if (vanishCoroutine != null) StopCoroutine(vanishCoroutine);
    }

}
