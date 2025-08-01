using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    public static InputManager Instance;

    //Context ���� ������
    InputContext currentContext = InputContext.Boot;
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
        //Debug.Log("H: " + H);
        //Debug.Log("D: " + D);
        //�̺�Ʈ ����
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
    //�̺�Ʈ ����
    private void OnEnable()
    {
        //Context ���� �̺�Ʈ
        InputEvents.OnContextUpdate += ContextManage;
        //�г� �� ��ư�� ��Ŀ��
        InputEvents.OnSelectFirstSelectable += SelectFirstButton;

        //���̺�&�ε� �̺�Ʈ�� �Է� �޴� �� ���� �� ��
        SystemEvents.OnSaveRequest += () => BlockInput();
        SystemEvents.OnSaveEnd += UnblockInput;
        SystemEvents.OnDataLoadStart += BlockInput;
        SystemEvents.OnDataLoadFinished += UnblockInput;
    }
    private void OnDisable()
    {
        //Context ���� �̺�Ʈ
        InputEvents.OnContextUpdate -= ContextManage;
        //�г� �� ��ư�� ��Ŀ��
        InputEvents.OnSelectFirstSelectable -= SelectFirstButton;

        //���̺�&�ε� �̺�Ʈ�� �Է� �޴� �� ���� �� ��
        SystemEvents.OnSaveRequest -= () => BlockInput();
        SystemEvents.OnSaveEnd -= UnblockInput;
        SystemEvents.OnDataLoadStart -= BlockInput;
        SystemEvents.OnDataLoadFinished -= UnblockInput;

    }
    //Context ���� �̺�Ʈ
    void ContextManage(InputContext context, bool state)
    {
        //���� ���ؽ�Ʈ�� On�� ��
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
        //���� ���ؽ�Ʈ�� Off�� ��
        else
        {
            currentContext = InputContext.Player;
            currentContextState = true;
        }        
    }
    //�г� �� ��ư�� ��Ŀ��
    void SelectFirstButton(GameObject panel)
    {
        EventSystem.current.SetSelectedGameObject(null);

        Selectable firstSelectable = panel.GetComponentInChildren<Selectable>();
        if(firstSelectable != null)
        {
            firstSelectable.Select();
        }

    }

    //���̺�&�ε� �̺�Ʈ�� �Է� �޴� �� ���� �� ��
    void BlockInput(int whatever = 1)
    {
        isInputPossible = !isInputPossible;
    }
    void UnblockInput()
    {
        isInputPossible = !isInputPossible;
    }






}
