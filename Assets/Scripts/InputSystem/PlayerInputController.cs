using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    //�� ������Ʈ
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

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //�̵� �̺�Ʈ ����
        InputEvents.Player.OnMove += Move;
        //���� �̺�Ʈ ����
        InputEvents.Player.OnJump += Jump;
        //���� Ȧ�� �̺�Ʈ ����
        InputEvents.Player.OnJumpHold += Jump_Hold;
        //�뽬 �̺�Ʈ ����
        InputEvents.Player.OnDash += Dash;
        //�и� �̺�Ʈ ����
        InputEvents.Player.OnParry += Parry;
        //ESC �̺�Ʈ (�ý��� �޴� ����) ����
        InputEvents.Player.OnCancel += Cancel;
        //Interact �̺�Ʈ ����
        InputEvents.Player.OnInteract += E;
    }
    private void OnDisable()
    {
        //�̵� �̺�Ʈ ����
        InputEvents.Player.OnMove -= Move;
        //���� �̺�Ʈ ����
        InputEvents.Player.OnJump -= Jump;
        //���� Ȧ�� �̺�Ʈ ����
        InputEvents.Player.OnJumpHold -= Jump_Hold;
        //�뽬 �̺�Ʈ ����
        InputEvents.Player.OnDash -= Dash;
        //�и� �̺�Ʈ ����
        InputEvents.Player.OnParry -= Parry;
        //ESC �̺�Ʈ (�ý��� �޴� ����) ����
        InputEvents.Player.OnCancel -= Cancel;
        //Interact �̺�Ʈ ����
        InputEvents.Player.OnInteract -= E;
    }

    //�̵� �̺�Ʈ ����
    void Move(float h)
    {
        playerMove.Move(h);
    }
    //���� �̺�Ʈ ����
    void Jump(bool j)
    {
        if (j) 
            playerJump.Jump(j);
    }
    //���� Ȧ�� �̺�Ʈ ����
    void Jump_Hold(bool j_Hold)
    {
        playerJump.Jump_Hold(j_Hold);
    }
    //�뽬 �̺�Ʈ ����
    void Dash(bool d)
    {
        if (d)
            playerDash.Dash(d);
    }
    //�и� �̺�Ʈ ����
    void Parry(bool p)
    {
        if (p)
            playerParry.Parry(p);
    }
    //ESC �̺�Ʈ (�ý��� �޴� ����) ����
    void Cancel(bool esc)
    {
        if(esc)
            playerUIInput.ESC(esc);
    }
    //Interact �̺�Ʈ ����
    void E(bool e)
    {
        if (e)
            playerInteraction.E(e);
    }




}
