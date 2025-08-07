using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBallShooter : MonoBehaviour, IDamageable
{
    //�� ������Ʈ
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    //�� �ڽ� ������Ʈ
    [SerializeField] private TrapSensor detectCollider;
    [SerializeField] private GameObject activeGroundArea;
    [SerializeField] private GameObject destroyGroundArea;

    //�߻� ���� ����Ʈ
    [SerializeField] private Transform firePoint;

    // �߻�ü ������
    public GameObject ballPrefab;
    public GameObject explosionPrefab;

    //�ı� �� ��������Ʈ
    [SerializeField] private Sprite destroyImage;


    //�ı��Ǿ����� �ƴ��� �÷���
    bool isDestroy;

    //�߻� ����
    bool isFireCoolTime;
    float FireCoolTime = 2f;
    float FireCoolTimer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (firePoint == null) firePoint = transform.Find("FirePoint");
        if (detectCollider == null) detectCollider = transform.Find("DetectCollider").GetComponent<TrapSensor>();
        if (activeGroundArea == null) activeGroundArea = transform.Find("GroundArea1").gameObject;
        if (destroyGroundArea == null) destroyGroundArea = transform.Find("GroundArea2").gameObject;

    }

    private void FixedUpdate()
    {
        //�߻� ��Ÿ�� ����
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

    //�߻�
    void Fire()
    {
        GameObject ball = Instantiate(ballPrefab, firePoint.position, Quaternion.identity);
        ball.GetComponent<TrapBall>().SetTarget(detectCollider.detectedTarget, gameObject);

    }
    //IDamageable�������̽� ����
    public void TakeDamage(Vector2 pos, int damage)
    {
        isDestroy = true;
        spriteRenderer.sprite = destroyImage;
        boxCollider.enabled = false;
        activeGroundArea.GetComponent<BoxCollider2D>().enabled = false;
        destroyGroundArea.GetComponent<BoxCollider2D>().enabled = true;
        GameObject explosion = Instantiate(explosionPrefab, firePoint.position, Quaternion.identity);
        
    }
}
