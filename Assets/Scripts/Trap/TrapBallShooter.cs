using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBallShooter : MonoBehaviour, IDamageable
{
    //내 컴포넌트
    SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D activeCollider;
    [SerializeField] private BoxCollider2D destroyCollider;

    //내 자식 오브젝트
    [SerializeField] private TrapSensor detectCollider;

    //발사 시작 포인트
    [SerializeField] private Transform firePoint;

    // 발사체 프리팹
    public GameObject ballPrefab;
    public GameObject explosionPrefab;

    //파괴 시 스프라이트
    [SerializeField] private Sprite destroyImage;


    //파괴되었는지 아닌지 플래그
    bool isDestroy;

    //발사 간격
    bool isFireCoolTime;
    float FireCoolTime = 2f;
    float FireCoolTimer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (firePoint == null) firePoint = transform.Find("FirePoint");
        if (detectCollider == null) detectCollider = transform.Find("DetectCollider").GetComponent<TrapSensor>();

    }

    private void FixedUpdate()
    {
        //발사 쿨타임 적용
        if (!isDestroy && !isFireCoolTime && detectCollider.detectedTarget != null)
        {
            FireCoolTimer = FireCoolTime;
            isFireCoolTime = true;

            Fire();
        }
        if (isFireCoolTime)
        {
            if (FireCoolTimer > 0)
            {
                FireCoolTimer -= Time.fixedDeltaTime;
            }
            else
            {
                FireCoolTimer = 0;
                isFireCoolTime = false;
            }
        }
    }

    //발사
    void Fire()
    {
        GameObject ball = Instantiate(ballPrefab, firePoint.position, Quaternion.identity);
        ball.GetComponent<TrapBall>().SetTarget(detectCollider.detectedTarget, gameObject);

    }
    //IDamageable인터페이스 구현
    public void TakeDamage(Vector2 pos, int damage)
    {
        isDestroy = true;
        spriteRenderer.sprite = destroyImage;
        activeCollider.enabled = false;
        destroyCollider.enabled = true;
        GameObject explosion = Instantiate(explosionPrefab, firePoint.position, Quaternion.identity);
        
    }
}
