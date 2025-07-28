using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    //내 컴포넌트
    Player player;
    PlayerMove playerMove;
    PlayerJump playerJump;
    PlayerDash playerDash;
    PlayerParry playerParry;
    PlayerUIInput playerUIInput;
    PlayerInteraction playerInteraction;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerMove = GetComponent<PlayerMove>();
        playerJump = GetComponent<PlayerJump>();
        playerDash = GetComponent<PlayerDash>();
        playerParry = GetComponent<PlayerParry>();
        playerUIInput = GetComponent<PlayerUIInput>();
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    //이벤트 구독
    private void OnEnable()
    {
        //이동 이벤트 구독
        InputEvents.Player.OnMove += Move;
        //점프 이벤트 구독
        InputEvents.Player.OnJump += Jump;
        //점프 홀딩 이벤트 구독
        InputEvents.Player.OnJumpHold += Jump_Hold;
        //대쉬 이벤트 구독
        InputEvents.Player.OnDash += Dash;
        //패링 이벤트 구독
        InputEvents.Player.OnParry += Parry;
        //ESC 이벤트 (시스템 메뉴 열기) 구독
        InputEvents.Player.OnCancel += Cancel;
        //Interact 이벤트 구독
        InputEvents.Player.OnInteract += E;
    }
    private void OnDisable()
    {
        //이동 이벤트 구독
        InputEvents.Player.OnMove -= Move;
        //점프 이벤트 구독
        InputEvents.Player.OnJump -= Jump;
        //점프 홀딩 이벤트 구독
        InputEvents.Player.OnJumpHold -= Jump_Hold;
        //대쉬 이벤트 구독
        InputEvents.Player.OnDash -= Dash;
        //패링 이벤트 구독
        InputEvents.Player.OnParry -= Parry;
        //ESC 이벤트 (시스템 메뉴 열기) 구독
        InputEvents.Player.OnCancel -= Cancel;
        //Interact 이벤트 구독
        InputEvents.Player.OnInteract -= E;
    }

    //이동 이벤트 구독
    void Move(float h)
    {
        playerMove.Move(h);
    }
    //점프 이벤트 구독
    void Jump(bool j)
    {
        if (j) 
            playerJump.Jump(j);
    }
    //점프 홀딩 이벤트 구독
    void Jump_Hold(bool j_Hold)
    {
        playerJump.Jump_Hold(j_Hold);
    }
    //대쉬 이벤트 구독
    void Dash(bool d)
    {
        if (d)
            playerDash.Dash(d);
    }
    //패링 이벤트 구독
    void Parry(bool p)
    {
        if (p)
            playerParry.Parry(p);
    }
    //ESC 이벤트 (시스템 메뉴 열기) 구독
    void Cancel(bool esc)
    {
        if(esc)
            playerUIInput.ESC(esc);
    }
    //Interact 이벤트 구독
    void E(bool e)
    {
        if (e)
            playerInteraction.E(e);
    }




}
