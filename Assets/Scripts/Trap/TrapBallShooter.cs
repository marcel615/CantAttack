using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBallShooter : MonoBehaviour
{
    //발사 시작 포인트
    [SerializeField] private Transform firePoint;

    // 발사체 프리팹
    public GameObject ballPrefab;

    //발사 간격
    bool isFireCoolTime;
    float FireCoolTime = 2f;
    float FireCoolTimer;

    //플레이어 감지 변수
    GameObject detectedTarget;



    private void Awake()
    {
        if (firePoint == null) firePoint = transform.Find("FirePoint");
        
    }

    void Update()
    {
        //발사 쿨타임 적용
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
        //플레이어 감지 시 detectedTarget에 저장
        if (collision.CompareTag("Player"))
        {
            detectedTarget = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //플레이어 감지 시 detectedTarget에 저장되어 있는 값 삭제
        if (collision.CompareTag("Player") && collision.gameObject == detectedTarget)
        {
            detectedTarget = null;
        }
    }

    //발사
    void Fire()
    {
        GameObject ball = Instantiate(ballPrefab, firePoint.position, Quaternion.identity);
        ball.GetComponent<TrapBall>().SetTarget(detectedTarget);


    }
}
