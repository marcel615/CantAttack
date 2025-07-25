using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    //내 컴포넌트
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
    }

    //이동 이벤트 구독
    void Move(float h)
    {
        playerMove.Move(h);
    }
    //점프 이벤트 구독
    void Jump(bool j)
    {
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
        playerAbility.Dash(d);
    }
    //패링 이벤트 구독
    void Parry(bool p)
    {
        playerAbility.Parry(p);
    }




}
