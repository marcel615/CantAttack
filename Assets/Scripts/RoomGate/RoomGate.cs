using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGate : MonoBehaviour
{
    Animator animator;
    [SerializeField] private BoxCollider2D deActivatedCollider;
    [SerializeField] private BoxCollider2D ActivatedCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //이벤트 구독
    private void OnEnable()
    {
        //문 Active 요청 이벤트
        GameEvents.OnRoomGateActive += OnGateActive;
        //문 DeActive 요청 이벤트
        GameEvents.OnRoomGateDeActive += OnGateDeActive;
    }
    private void OnDisable()
    {
        //문 Active 요청 이벤트
        GameEvents.OnRoomGateActive -= OnGateActive;
        //문 DeActive 요청 이벤트
        GameEvents.OnRoomGateDeActive -= OnGateDeActive;
    }
    void OnGateActive(string eventID)
    {
        //문 닫힘
        animator.SetTrigger("isActivate");
        deActivatedCollider.enabled = false;
        ActivatedCollider.enabled = true;
    }
    void OnGateDeActive(string eventID)
    {
        //문 열림
        animator.SetTrigger("isDeActivate");
        deActivatedCollider.enabled = true;
        ActivatedCollider.enabled = false;
    }

}
