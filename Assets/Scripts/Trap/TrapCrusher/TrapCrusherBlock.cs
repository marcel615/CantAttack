using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCrusherBlock : MonoBehaviour
{
    //�� ������Ʈ
    Rigidbody2D rigid;
    BoxCollider2D bodyCollider;

    //�ڽ� ������Ʈ
    [SerializeField] private GameObject damageCollider;
    [SerializeField] private GameObject groundCollider;


    //�⺻ ����
    [SerializeField] private int damage = 1;

    //�ݶ��̴�

    //�����̴� ���� ���� ����
    Vector3 originPos;
    bool isMoveCycleOver;
    Vector2 RePositionDir;
    float ResetSpeed = 3f;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();

        if (damageCollider == null) damageCollider = transform.Find("DamageArea")?.gameObject;
        if (groundCollider == null) groundCollider = transform.Find("GroundArea")?.gameObject;

        originPos = transform.position;
    }
    private void FixedUpdate()
    {
        if (!isMoveCycleOver && rigid.velocity.y == 0)
        {
            isMoveCycleOver = true;

            //bodyCollider.enabled = true;
            damageCollider.GetComponent<BoxCollider2D>().enabled = false;

            ResetPosition();
        }
        if (isMoveCycleOver)
        {
            if (transform.position.y < originPos.y)
            {
                rigid.velocity = RePositionDir * ResetSpeed;
            }
            else
            {
                isMoveCycleOver = false;

                //bodyCollider.enabled = false;
                damageCollider.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

    }
    void ResetPosition()
    {
        RePositionDir = (originPos - transform.position).normalized;
    }


}
