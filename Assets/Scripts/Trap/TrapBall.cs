using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrapBall : MonoBehaviour, IParryable
{
    //�� ������Ʈ
    Rigidbody2D rigid;

    //�߻� ����
    GameObject Target;  //�߻� ���
    GameObject Sender;  //�߻� ��û�� ��
    Vector2 direction;  //�߻� ����
    float speed = 12f;   //�߻� �ӵ�
    float vanishTime = 3f; //���� �ð�

    //Destroy�Ǵ� �ð� �ڷ�ƾ
    Coroutine vanishCoroutine;

    //�⺻ ����
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

    //Ÿ������ �����ϱ�
    public void SetTarget(GameObject target, GameObject sender)
    {
        Target = target;
        Sender = sender;
        SetDirection();
        Shoot();
    }
    //�и� �������̽� ����
    public void OnParried(GameObject parryOrigin)
    {
        CancelDestroy();
        SetTarget(Sender, parryOrigin);
    }
    //�߻� ���� ���ϱ�
    void SetDirection()
    {
        direction = (Target.transform.position - transform.position).normalized; //���� normalize�ϱ�
    }
    //�߻��ϱ�
    void Shoot()
    {
        rigid.velocity = direction * speed;
        vanishCoroutine = StartCoroutine(VanishAfterTime());
    }

    //Destroy Ÿ�̸� ����
    private IEnumerator VanishAfterTime()
    {
        yield return new WaitForSeconds(vanishTime);
        Destroy(gameObject);
    }
    void CancelDestroy()
    {
        // ���� Ÿ�̸� ���
        if (vanishCoroutine != null)
        {
            StopCoroutine(vanishCoroutine);
        }
    }


}
