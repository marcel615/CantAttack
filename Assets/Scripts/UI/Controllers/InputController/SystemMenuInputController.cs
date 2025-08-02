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
        //SystemMenuOpen �̺�Ʈ ����
        InputEvents.SystemMenu.OnSystemMenuOpen += SystemMenuOpen;
        //SystemMenuClose �̺�Ʈ ����
        InputEvents.SystemMenu.OnSystemMenuClose += SystemMenuClose;
        //Cancel �̺�Ʈ ����
        InputEvents.SystemMenu.OnCancel += ESC;
        //Submit �̺�Ʈ ����
        InputEvents.SystemMenu.OnSubmit += Enter;
        //Interact �̺�Ʈ ����
        InputEvents.SystemMenu.OnInteract += E;
    }
    private void OnDisable()
    {
        //SystemMenuOpen �̺�Ʈ ����
        InputEvents.SystemMenu.OnSystemMenuOpen -= SystemMenuOpen;
        //SystemMenuClose �̺�Ʈ ����
        InputEvents.SystemMenu.OnSystemMenuClose -= SystemMenuClose;
        //�̵� �̺�Ʈ ����
        InputEvents.SystemMenu.OnCancel -= ESC;
        //Submit �̺�Ʈ ����
        InputEvents.SystemMenu.OnSubmit -= Enter;
        //Interact �̺�Ʈ ����
        InputEvents.SystemMenu.OnInteract -= E;
    }

    //SystemMenuOpen �̺�Ʈ ����
    void SystemMenuOpen(InputContext sourceInputContext)
    {
        systemMenu.SystemMenuOpen(sourceInputContext);
    }
    //SystemMenuClose �̺�Ʈ ����
    void SystemMenuClose(InputContext sourceInputContext)
    {
        systemMenu.SystemMenuClose(sourceInputContext);
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
