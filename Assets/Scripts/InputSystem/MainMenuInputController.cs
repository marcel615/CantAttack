using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInputController : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private MainMenu mainMenu;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (mainMenu == null) mainMenu = transform.Find("UICanvas/MainMenuPanel")?.GetComponent<MainMenu>();

    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //MainMenuOpen �̺�Ʈ ����
        InputEvents.MainMenu.OnMainMenuOpen += MainMenuOpen;
        //Cancel �̺�Ʈ ����
        InputEvents.MainMenu.OnCancel += ESC;
        //Submit �̺�Ʈ ����
        InputEvents.MainMenu.OnSubmit += Enter;
        //Interact �̺�Ʈ ����
        InputEvents.MainMenu.OnInteract += E;
    }
    private void OnDisable()
    {
        //MainMenuOpen �̺�Ʈ ����
        InputEvents.MainMenu.OnMainMenuOpen -= MainMenuOpen;
        //�̵� �̺�Ʈ ����
        InputEvents.MainMenu.OnCancel -= ESC;
        //Submit �̺�Ʈ ����
        InputEvents.MainMenu.OnSubmit -= Enter;
        //Interact �̺�Ʈ ����
        InputEvents.MainMenu.OnInteract -= E;
    }

    //SettingOpen �̺�Ʈ ����
    void MainMenuOpen(InputContext sourceInputContext)
    {
        mainMenu.MainMenuOpen(sourceInputContext);
    }

    //Cancel �̺�Ʈ ����
    void ESC(bool esc)
    {
        if (esc)
            mainMenu.ESC(esc);
    }
    //Submit �̺�Ʈ ����
    void Enter(bool enter)
    {
        if (enter)
            mainMenu.Enter(enter);
    }
    //Interact �̺�Ʈ ����
    void E(bool e)
    {
        if (e)
            mainMenu.E(e);
    }

}
