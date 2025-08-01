using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static InputManager Instance;

    //Context 관리 변수들
    InputContext currentContext = InputContext.Boot;
    bool currentContextState = true;

    //키 입력 가능한지 플래그
    bool isInputPossible = true;

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
        if (!isInputPossible) return; //입력 불가능한 상태면 입력 못받도록

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
        //Debug.Log("H: " + H);
        //Debug.Log("D: " + D);
        //이벤트 발행
        switch (currentContext)
        {
            case InputContext.Boot:

                break;

            case InputContext.Player:
                InputEvents.Player.InvokeMove(H);
                InputEvents.Player.InvokeJump(J);
                InputEvents.Player.InvokeJumpHold(J_ing);
                InputEvents.Player.InvokeDash(D);
                InputEvents.Player.InvokeParry(P);
                InputEvents.Player.InvokeCancel(Esc);
                InputEvents.Player.InvokeInteract(E);


                break;

            case InputContext.SystemMenu:
                InputEvents.SystemMenu.InvokeCancel(Esc);
                InputEvents.SystemMenu.InvokeSubmit(Enter);
                InputEvents.SystemMenu.InvokeInteract(E);

                break;

            case InputContext.Setting:
                InputEvents.Setting.InvokeCancel(Esc);
                InputEvents.Setting.InvokeSubmit(Enter);
                InputEvents.Setting.InvokeInteract(E);

                break;

            case InputContext.MainMenu:
                InputEvents.MainMenu.InvokeCancel(Esc);
                InputEvents.MainMenu.InvokeSubmit(Enter);
                InputEvents.MainMenu.InvokeInteract(E);

                break;

            case InputContext.SaveSlot:
                InputEvents.SaveSlot.InvokeCancel(Esc);
                InputEvents.SaveSlot.InvokeSubmit(Enter);
                InputEvents.SaveSlot.InvokeInteract(E);

                break;

            case InputContext.Dialogue:
                InputEvents.Dialogue.InvokeCancel(Esc);
                InputEvents.Dialogue.InvokeSubmit(Enter);
                InputEvents.Dialogue.InvokeInteract(E);

                break;

            case InputContext.SceneChange:

                break;

            case InputContext.Fade:

                break;

                //InputEvents.InvokeUseHealItem(R);

        }

    }
    //이벤트 구독
    private void OnEnable()
    {
        //Context 변경 이벤트
        InputEvents.OnContextUpdate += ContextManage;
        //패널 내 버튼에 포커스
        InputEvents.OnSelectFirstSelectable += SelectFirstButton;

        //세이브&로드 이벤트로 입력 받는 거 제한 걸 때
        SystemEvents.OnSaveRequest += () => BlockInput();
        SystemEvents.OnSaveEnd += UnblockInput;
        SystemEvents.OnDataLoadStart += BlockInput;
        SystemEvents.OnDataLoadFinished += UnblockInput;
    }
    private void OnDisable()
    {
        //Context 변경 이벤트
        InputEvents.OnContextUpdate -= ContextManage;
        //패널 내 버튼에 포커스
        InputEvents.OnSelectFirstSelectable -= SelectFirstButton;

        //세이브&로드 이벤트로 입력 받는 거 제한 걸 때
        SystemEvents.OnSaveRequest -= () => BlockInput();
        SystemEvents.OnSaveEnd -= UnblockInput;
        SystemEvents.OnDataLoadStart -= BlockInput;
        SystemEvents.OnDataLoadFinished -= UnblockInput;

    }
    //Context 변경 이벤트
    void ContextManage(InputContext context, bool state)
    {
        //받은 컨텍스트가 On일 때
        if (state)
        {
            currentContext = context;
            currentContextState = state;
            if(context == InputContext.Player)
            {
                Time.timeScale = 1;
            }
            else
            {
                if(context != InputContext.SceneChange)
                {
                    Time.timeScale = 0;
                }
            }
        }
        //받은 컨텍스트가 Off일 때
        else
        {
            currentContext = InputContext.Player;
            currentContextState = true;
        }        
    }
    //패널 내 버튼에 포커스
    void SelectFirstButton(GameObject panel)
    {
        EventSystem.current.SetSelectedGameObject(null);

        Selectable firstSelectable = panel.GetComponentInChildren<Selectable>();
        if(firstSelectable != null)
        {
            firstSelectable.Select();
        }

    }

    //세이브&로드 이벤트로 입력 받는 거 제한 걸 때
    void BlockInput(int whatever = 1)
    {
        isInputPossible = !isInputPossible;
    }
    void UnblockInput()
    {
        isInputPossible = !isInputPossible;
    }






}
