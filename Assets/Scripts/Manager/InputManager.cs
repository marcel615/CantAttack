using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    private static InputManager Instance;

    //Context 관리 변수들
    InputContext currentContext;
    bool currentContextState = true;

    //키 입력 추적 변수들
    float H; //좌우
    bool J; //점프
    bool J_ing; //점프버튼 누르고 있는지
    bool D; //대쉬
    bool P; //패링

    bool Esc; //Cancel
    bool Enter; //Submit
    bool E; //Interact

    bool R; //UseHealItem



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

        Esc = Input.GetButtonDown("Cancel");
        Enter = Input.GetButtonDown("Submit");
        E = Input.GetButtonDown("Interact");

        R = Input.GetButtonDown("UseHealItem");

        //이벤트 발행
        switch (currentContext)
        {
            case InputContext.Player:
                InputEvents.InvokeMove(H);
                InputEvents.InvokeJump(J);
                InputEvents.InvokeJumpHold(J_ing);
                InputEvents.InvokeDash(D);
                InputEvents.InvokeParry(P);

                //SystemMenu 진입이벤트
                InputEvents.InvokeCancel(Esc);

                break;

            case InputContext.SystemMenu:
                InputEvents.InvokeCancel(Esc);
                InputEvents.InvokeSubmit(Enter);
                InputEvents.InvokeInteract(E);

                break;

            case InputContext.Setting:
                InputEvents.InvokeCancel(Esc);
                InputEvents.InvokeSubmit(Enter);
                InputEvents.InvokeInteract(E);

                break;

                //InputEvents.InvokeUseHealItem(R);

        }




    }
    //이벤트 구독
    private void OnEnable()
    {
        InputEvents.OnContextUpdate += ContextManage;
    }
    private void OnDisable()
    {
        InputEvents.OnContextUpdate -= ContextManage;

    }
    void ContextManage(InputContext context, bool state)
    {
        //받은 컨텍스트가 On일 때
        if (state)
        {
            currentContext = context;
            currentContextState = state;
        }
        //받은 컨텍스트가 Off일 때
        else
        {
            currentContext = InputContext.Player;
            currentContextState = true;
        }

        
    }







}
