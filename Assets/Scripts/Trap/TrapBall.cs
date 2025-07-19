using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrapBall : MonoBehaviour
{
    //�� ������Ʈ
    Rigidbody2D rigid;

    //�߻� ����
    GameObject Target;  //�߻� ���
    Vector2 direction;  //�߻� ����
    float speed = 12f;   //�߻� �ӵ�
    float vanishTime = 3f; //���� �ð�

    //�⺻ ����
    int damage = 1;

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
        if (collision.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(transform.position, damage);
            Destroy(gameObject);
        }
    }

    //Ÿ������ ��������
    public void SetTarget(GameObject target)
    {
        Target = target;
        SetDirection();
    }
    //�߻� ���� ���ϱ�
    void SetDirection()
    {
        direction = (Target.transform.position - transform.position).normalized; //���� normalize�ϱ�

    }
}
