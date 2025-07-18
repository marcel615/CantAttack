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
