using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapSawMover : MonoBehaviour
{
    //내 컴포넌트
    Rigidbody2D rigid;

    //움직임 관련하여 오브젝트마다 튜닝 변수
    [SerializeField] private Transform targetPoint;
    public float moveSpeed = 2f;
    public float waitTime = 1f;

    //움직임 관련 변수
    Vector2 startPos;
    Vector2 endPos;
    float waitTimer;
    Vector2 targetPos;
    bool isMoveToEnd;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        //시작 위치 지정
        startPos = transform.position;
        //도착 위치 지정
        if (targetPoint != null)
            endPos = targetPoint.position;
        else endPos = transform.position;

        isMoveToEnd = true;
    }

    private void FixedUpdate()
    {
        //기다리는 시간
        if(waitTimer > 0)
        {
            waitTimer -= Time.fixedDeltaTime;
            return;
        }
        //움직일 방향 설정
        targetPos = isMoveToEnd ? endPos : startPos;

        //움직이는 로직
        if (Vector2.Distance(transform.position, targetPos) > 0.01f)
        {
            rigid.MovePosition(Vector2.MoveTowards(rigid.position, targetPos, moveSpeed * Time.fixedDeltaTime));
        }
        else //다 움직였으면 움직일 방향 토글하고 기다리는 시간 활성화
        {
            isMoveToEnd = !isMoveToEnd;
            waitTimer = waitTime;
        }
    }


}
