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

    private void Awake()
    {
        player = GetComponent<Player>();
        playerMove = GetComponent<PlayerMove>();
        playerJump = GetComponent<PlayerJump>();
        playerAbility = GetComponent<PlayerAbility>();
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //�̵� �̺�Ʈ ����
        InputEvents.OnMove += Move;
        //���� �̺�Ʈ ����
        InputEvents.OnJump += Jump;
        //���� Ȧ�� �̺�Ʈ ����
        InputEvents.OnJumpHold += Jump_Hold;
        //�뽬 �̺�Ʈ ����
        InputEvents.OnDash += Dash;
        //�и� �̺�Ʈ ����
        InputEvents.OnParry += Parry;
    }
    private void OnDisable()
    {
        //�̵� �̺�Ʈ ����
        InputEvents.OnMove -= Move;
        //���� �̺�Ʈ ����
        InputEvents.OnJump -= Jump;
        //���� Ȧ�� �̺�Ʈ ����
        InputEvents.OnJumpHold -= Jump_Hold;
        //�뽬 �̺�Ʈ ����
        InputEvents.OnDash -= Dash;
        //�и� �̺�Ʈ ����
        InputEvents.OnParry -= Parry;
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




}
