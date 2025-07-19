using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBallShooter : MonoBehaviour
{
    //�߻� ���� ����Ʈ
    [SerializeField] private Transform firePoint;

    // �߻�ü ������
    public GameObject ballPrefab;

    //�߻� ����
    bool isFireCoolTime;
    float FireCoolTime = 2f;
    float FireCoolTimer;

    //�÷��̾� ���� ����
    GameObject detectedTarget;



    private void Awake()
    {
        if (firePoint == null) firePoint = transform.Find("FirePoint");
        
    }

    void Update()
    {
        //�߻� ��Ÿ�� ����
        if (!isFireCoolTime && detectedTarget != null)
        {
            FireCoolTimer = FireCoolTime;
            isFireCoolTime = true;

            Fire();

        }
        if (isFireCoolTime)
        {
            if(FireCoolTimer > 0)
            {
                FireCoolTimer -= Time.deltaTime;
            }
            else
            {
                FireCoolTimer = 0;
                isFireCoolTime = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�÷��̾� ���� �� detectedTarget�� ����
        if (collision.CompareTag("Player"))
        {
            detectedTarget = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //�÷��̾� ���� �� detectedTarget�� ����Ǿ� �ִ� �� ����
        if (collision.CompareTag("Player") && collision.gameObject == detectedTarget)
        {
            detectedTarget = null;
        }
    }

    //�߻�
    void Fire()
    {
        GameObject ball = Instantiate(ballPrefab, firePoint.position, Quaternion.identity);
        ball.GetComponent<TrapBall>().SetTarget(detectedTarget);


    }
}
