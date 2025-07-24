using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemMenuInputController : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private SystemMenu systemMenu;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (systemMenu == null) systemMenu = transform.Find("UICanvas/SystemMenuPanel")?.GetComponent<SystemMenu>();

    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //Cancel �̺�Ʈ ����
        InputEvents.OnCancel += ESC;
        //Submit �̺�Ʈ ����
        InputEvents.OnSubmit += Enter;
        //Interact �̺�Ʈ ����
        InputEvents.OnInteract += E;
    }
    private void OnDisable()
    {
        //�̵� �̺�Ʈ ����
        InputEvents.OnCancel -= ESC;
        //Submit �̺�Ʈ ����
        InputEvents.OnSubmit -= Enter;
        //Interact �̺�Ʈ ����
        InputEvents.OnInteract -= E;
    }

    //Cancel �̺�Ʈ ����
    void ESC(bool esc)
    {
        if (esc) 
            systemMenu.ESC(esc);
    }
    //Submit �̺�Ʈ ����
    void Enter(bool enter)
    {
        if(enter)
            systemMenu.Enter(enter);
    }
    //Interact �̺�Ʈ ����
    void E(bool e)
    {
        if (e)
            systemMenu.E(e);
    }

}
