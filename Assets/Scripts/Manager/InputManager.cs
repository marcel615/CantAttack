using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    private static InputManager Instance;

    //PlayerInputController
    //UIInputController

    //키 입력 추적 변수들
    float H; //좌우
    bool J; //점프
    bool J_ing; //점프버튼 누르고 있는지
    bool D; //대쉬
    bool P; //패링


    private void Awake()
    {
        // 기존 인스턴스가 존재할 때 && 지금 새로운 인스턴스가 생성되려고 할 때
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //중복되지 않도록 지금 새롭게 생성되는 놈은 파괴시킨다
            return;
        }
        // 인스턴스 처음 할당
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //사용자 입력들 받아오기
        H = Input.GetAxisRaw("Horizontal");
        J = Input.GetButtonDown("Jump");
        J_ing = Input.GetButton("Jump");
        D = Input.GetButtonDown("Dash");
        P = Input.GetButtonDown("Parry");

        //이벤트 발행
        InputEvents.InvokeMove(H);
        InputEvents.InvokeJump(J);
        InputEvents.InvokeJumpHold(J_ing);
        InputEvents.InvokeDash(D);
        InputEvents.InvokeParry(P);



    }






}
