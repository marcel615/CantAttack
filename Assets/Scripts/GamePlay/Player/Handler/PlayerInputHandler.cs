using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    //내 컴포넌트
    PlayerController playerController;

    //InputManager
    [SerializeField] private InputManager inputManager;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        if(inputManager == null) inputManager = InputManager.Instance;
    }
    private void Update()
    {
        //Move 입력 관련
        if (inputManager.currentContext == playerController.thisContext)
        {
            playerController.H = inputManager.H;
        }
        else
        {
            playerController.H = 0;
        }
    }
    //입력 이벤트 구독
    private void OnEnable()
    {
        //점프 입력 이벤트 구독
        InputEvents.Player.OnJump += playerController.OnJump;
        //점프 홀딩 입력 이벤트 구독
        InputEvents.Player.OnJumpHold += playerController.OnJumpHold;
        //대쉬 입력 이벤트 구독
        InputEvents.Player.OnDash += playerController.OnDash;
        //패링 입력 이벤트 구독
        InputEvents.Player.OnParry += playerController.OnParry;
        //ESC 입력 이벤트 (시스템 메뉴 열기) 구독
        InputEvents.Player.OnCancel += playerController.OnCancel;
        //Interact 입력 이벤트 구독
        InputEvents.Player.OnInteract += playerController.OnInteract;
        //Tab 입력 이벤트 구독
        InputEvents.Player.OnTab += playerController.OnTab;
        //ShieldSlot 선택 이벤트 (1번~4번 숫자키로 슬롯 선택)
        InputEvents.Player.OnShieldSlot += OnShieldSlot;
    }
    private void OnDisable()
    {
        //점프 입력 이벤트 구독
        InputEvents.Player.OnJump -= playerController.OnJump;
        //점프 홀딩 입력 이벤트 구독
        InputEvents.Player.OnJumpHold -= playerController.OnJumpHold;
        //대쉬 입력 이벤트 구독
        InputEvents.Player.OnDash -= playerController.OnDash;
        //패링 입력 이벤트 구독
        InputEvents.Player.OnParry -= playerController.OnParry;
        //ESC 입력 이벤트 (시스템 메뉴 열기) 구독
        InputEvents.Player.OnCancel -= playerController.OnCancel;
        //Interact 입력 이벤트 구독
        InputEvents.Player.OnInteract -= playerController.OnInteract;
        //Tab 입력 이벤트 구독
        InputEvents.Player.OnTab -= playerController.OnTab;
        //ShieldSlot 선택 이벤트 (1번~4번 숫자키로 슬롯 선택)
        InputEvents.Player.OnShieldSlot -= OnShieldSlot;
    }

    //ShieldSlot 선택 이벤트 (1번~4번 숫자키로 슬롯 선택)
    void OnShieldSlot(int index)
    {
        PlayerEvents.InvokeShieldSlotSelected(index);
    }


}
