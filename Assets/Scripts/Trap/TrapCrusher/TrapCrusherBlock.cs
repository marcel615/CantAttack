using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCrusherBlock : MonoBehaviour
{
    //내 컴포넌트
    Rigidbody2D rigid;
    BoxCollider2D bodyCollider;

    //자식 오브젝트
    [SerializeField] private GameObject damageCollider;
    [SerializeField] private GameObject groundCollider;


    //기본 변수
    [SerializeField] private int damage = 1;

    //콜라이더

    //움직이는 로직 관련 변수
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
