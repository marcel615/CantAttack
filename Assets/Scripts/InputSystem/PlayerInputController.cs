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
    PlayerAbility playerAbility;
    PlayerUIInput playerUIInput;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerMove = GetComponent<PlayerMove>();
        playerJump = GetComponent<PlayerJump>();
        playerAbility = GetComponent<PlayerAbility>();
        playerUIInput = GetComponent<PlayerUIInput>();
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
    }

    //�̵� �̺�Ʈ ����
    void Move(float h)
    {
        playerMove.Move(h);
    }
    //���� �̺�Ʈ ����
    void Jump(bool j)
    {
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
        playerAbility.Dash(d);
    }
    //�и� �̺�Ʈ ����
    void Parry(bool p)
    {
        playerAbility.Parry(p);
    }
    //ESC �̺�Ʈ (�ý��� �޴� ����) ����
    void Cancel(bool esc)
    {
        if(esc)
            playerUIInput.ESC(esc);
    }




}
