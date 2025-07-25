using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    private static InputManager Instance;

    //Context ���� ������
    InputContext currentContext;
    bool currentContextState = true;

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
                InputEvents.InvokeMove(H);
                InputEvents.InvokeJump(J);
                InputEvents.InvokeJumpHold(J_ing);
                InputEvents.InvokeDash(D);
                InputEvents.InvokeParry(P);

                //SystemMenu �����̺�Ʈ
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
    //�̺�Ʈ ����
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







}
