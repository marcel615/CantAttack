using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    //�ٸ� ��ü��
    public CameraManager mainCamera;

    //Ű �Է� ���� ������
    float H; //�¿�
    bool J; //����
    bool D; //�뽬
    bool P; //�и�

    //�⺻ ������
    float normalSpeed = 4f;

  

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void Start()
    {
        //���� ī�޶� ����
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
            mainCamera.SetTarget(transform);
        }
    }
    private void Update()
    {
        //����� �Էµ� �޾ƿ���
        H = Input.GetAxisRaw("Horizontal");
        J = Input.GetButtonDown("Jump");
        D = Input.GetButtonDown("Dash");
        P = Input.GetButtonDown("Parry");

        //H���� ���� �÷��̾� ĳ���Ͱ� �ٶ󺸴� ���� ����
        if(H != 0)
        {
            spriteRenderer.flipX = H < 0;
        }


    }
    private void FixedUpdate()
    {
        //�÷��̾� �⺻ ������ ����
        rigid.velocity = new Vector2(H * normalSpeed, rigid.velocity.y);
        
    }



}
