using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    private static InputManager Instance;

    //PlayerInputController
    //UIInputController

    //Ű �Է� ���� ������
    float H; //�¿�
    bool J; //����
    bool J_ing; //������ư ������ �ִ���
    bool D; //�뽬
    bool P; //�и�


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

        //�̺�Ʈ ����
        InputEvents.InvokeMove(H);
        InputEvents.InvokeJump(J);
        InputEvents.InvokeJumpHold(J_ing);
        InputEvents.InvokeDash(D);
        InputEvents.InvokeParry(P);



    }






}
