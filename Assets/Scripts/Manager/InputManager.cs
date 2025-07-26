using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    private static InputManager Instance;

    //Context ���� ������
    InputContext currentContext = InputContext.MainMenu;
    bool currentContextState = true;

    //Ű �Է� �������� �÷���
    bool isInputPossible = true;

    //Ű �Է� ���� ������
    float H; //�¿�
    bool J; //����
    bool J_ing; //������ư ������ �ִ���
    bool D; //�뽬
    bool P; //�и�

    bool Esc; //Cancel
    bool Enter; //Submit
    bool E; //Interact

    bool R; //UseHealItem



    private void Awake()
    {
        // ���� �ν��Ͻ��� ������ �� && ���� ���ο� �ν��Ͻ��� �����Ƿ��� �� ��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //�ߺ����� �ʵ��� ���� ���Ӱ� �����Ǵ� ���� �ı���Ų��
            return;
        }
        // �ν��Ͻ� ó�� �Ҵ�
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!isInputPossible) return; //�Է� �Ұ����� ���¸� �Է� ���޵���

        //����� �Էµ� �޾ƿ���
        H = Input.GetAxisRaw("Horizontal");
        J = Input.GetButtonDown("Jump");
        J_ing = Input.GetButton("Jump");
        D = Input.GetButtonDown("Dash");
        P = Input.GetButtonDown("Parry");

        Esc = Input.GetButtonDown("Cancel");
        Enter = Input.GetButtonDown("Submit");
        E = Input.GetButtonDown("Interact");

        R = Input.GetButtonDown("UseHealItem");

        //�̺�Ʈ ����
        switch (currentContext)
        {
            case InputContext.Player:
                if (Esc)
                    H = 0;  //UI �г� ������ �� Player �¿� ������ ���߱�
                InputEvents.Player.InvokeMove(H);
                InputEvents.Player.InvokeJump(J);
                InputEvents.Player.InvokeJumpHold(J_ing);
                InputEvents.Player.InvokeDash(D);
                InputEvents.Player.InvokeParry(P);
                InputEvents.Player.InvokeCancel(Esc);


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

                //InputEvents.InvokeUseHealItem(R);

        }




    }
    //�̺�Ʈ ����
    private void OnEnable()
    {
        InputEvents.OnContextUpdate += ContextManage;
        InputEvents.OnSelectFirstSelectable += SelectFirstButton;
        SystemEvents.OnDataLoadStart += BlockInput;
        SystemEvents.OnDataLoadFinished += UnblockInput;
    }
    private void OnDisable()
    {
        InputEvents.OnContextUpdate -= ContextManage;
        InputEvents.OnSelectFirstSelectable -= SelectFirstButton;
        SystemEvents.OnDataLoadStart -= BlockInput;
        SystemEvents.OnDataLoadFinished -= UnblockInput;

    }
    void ContextManage(InputContext context, bool state)
    {
        //���� ���ؽ�Ʈ�� On�� ��
        if (state)
        {
            currentContext = context;
            currentContextState = state;
        }
        //���� ���ؽ�Ʈ�� Off�� ��
        else
        {
            currentContext = InputContext.Player;
            currentContextState = true;
        }        
    }
    
    void SelectFirstButton(GameObject panel)
    {
        EventSystem.current.SetSelectedGameObject(null);

        Selectable firstSelectable = panel.GetComponentInChildren<Selectable>();
        if(firstSelectable != null)
        {
            firstSelectable.Select();
        }

    }
    void BlockInput(int whatever)
    {
        isInputPossible = !isInputPossible;
    }
    void UnblockInput()
    {
        isInputPossible = !isInputPossible;
    }






}
