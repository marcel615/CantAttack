using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TrapCrusherBlock : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private GameObject damageCollider;
    [SerializeField] private GameObject groundCollider;
    [SerializeField] private GameObject dropPosition;

    //기본 변수
    public int damage = 1;

    //움직이는 로직 관련 변수
    float waitTime = 1f;        //각 행동을 마치고 기다리는 시간
    Vector3 originPos;          //떨어지기 전 원래 위치
    Vector2 dropPos;            //떨어질 곳 위치
    Vector2 targetPos;          //각 상태에 맞추어 지정할 위치
    float ResetSpeed = 3f;      //다시 원래 위치로 리셋할 때의 스피드
    float dropSpeed = 15f;      //떨어지는 스피드
    float targetSpeed;          //각 상태에 맞추어 지정할 스피드

    //플래그
    bool isResetAndWait;        //리셋되고 떨어지기를 기다리는 시간인지 플래그
    bool isDropedAndWait;       //떨어지고 리셋되기를 기다리는 시간인지 플래그
    bool isStartMove;           //다 기다리고 움직이는 시간인지 플래그


    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (damageCollider == null) damageCollider = transform.Find("DamageArea")?.gameObject;
        if (dropPosition == null) dropPosition = transform.Find("DropPosition")?.gameObject;

        //각각의 Position 초기화
        originPos = transform.position;
        dropPos = dropPosition.transform.position;
    }
    private void FixedUpdate()
    {
        //기다렸다가 떨어지기 시작
        if(!isResetAndWait && Vector3.Distance(transform.position, originPos) < 0.01f)
        {
            StartCoroutine(WaitAndDrop(waitTime));
        }
        //기다렸다가 원래 위치로 리셋
        if(!isDropedAndWait && Vector3.Distance(transform.position, dropPos) < 0.01f)
        {
            StartCoroutine(WaitAndReset(waitTime));
        }
        if (isStartMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, targetSpeed * Time.fixedDeltaTime);
        }
    }
    private IEnumerator WaitAndDrop(float waitTime)
    {
        //플래그 세팅
        isResetAndWait = true;
        isDropedAndWait = false;
        //움직이기 플래그 세팅
        isStartMove = false;

        //데미지 콜라이더 잠시 끄고
        damageCollider.GetComponent<BoxCollider2D>().enabled = false;

        //waitTime만큼 기다렸다가
        yield return new WaitForSeconds(waitTime);

        //데미지 콜라이더 다시 키고
        damageCollider.GetComponent<BoxCollider2D>().enabled = true;

        //목표지점, 스피드 설정 후 움직이기 플래그 세팅
        targetPos = dropPos;
        targetSpeed = dropSpeed;
        isStartMove = true;
    }
    private IEnumerator WaitAndReset(float waitTime)
    {
        //플래그 세팅
        isDropedAndWait = true;
        isResetAndWait = false;
        //움직이기 플래그 세팅
        isStartMove = false;

        //데미지 콜라이더 잠시 끄고
        damageCollider.GetComponent<BoxCollider2D>().enabled = false;

        //waitTime만큼 기다렸다가
        yield return new WaitForSeconds(waitTime);

        //목표지점, 스피드 설정 후 움직이기 플래그 세팅
        targetPos = originPos;
        targetSpeed = ResetSpeed;
        isStartMove = true;
    }
}
