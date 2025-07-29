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

    //�⺻ ����
    [SerializeField] private int damage = 1;
    Coroutine vanishCoroutine;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rigid.velocity = direction * speed;
        vanishCoroutine = StartCoroutine(VanishAfterTime());

    }
    private IEnumerator VanishAfterTime()
    {
        yield return new WaitForSeconds(vanishTime);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(transform.position, damage);
            Destroy(gameObject);
        }
    }

    //Ÿ������ ��������
    public void SetTarget(GameObject target, GameObject sender)
    {
        Target = target;
        Sender = sender;
        SetDirection();
    }
    //�߻� ���� ���ϱ�
    void SetDirection()
    {
        direction = (Target.transform.position - transform.position).normalized; //���� normalize�ϱ�
    }

    //�и� �������̽� ����
    public void OnParried(GameObject parryOrigin)
    {
        Vector2 reflectDir = (Sender.transform.position - parryOrigin.transform.position).normalized;
        rigid.velocity = reflectDir * speed;

        // ���� Ÿ�̸� ���
        if (vanishCoroutine != null)
        {
            StopCoroutine(vanishCoroutine);
        }

        vanishCoroutine = StartCoroutine(VanishAfterTime());
    }


}
