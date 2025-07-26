using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInputController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private MainMenu mainMenu;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (mainMenu == null) mainMenu = transform.Find("UICanvas/MainMenuPanel")?.GetComponent<MainMenu>();

    }

    //이벤트 구독
    private void OnEnable()
    {
        //MainMenuOpen 이벤트 구독
        InputEvents.MainMenu.OnMainMenuOpen += MainMenuOpen;
        //Cancel 이벤트 구독
        InputEvents.MainMenu.OnCancel += ESC;
        //Submit 이벤트 구독
        InputEvents.MainMenu.OnSubmit += Enter;
        //Interact 이벤트 구독
        InputEvents.MainMenu.OnInteract += E;
    }
    private void OnDisable()
    {
        //MainMenuOpen 이벤트 구독
        InputEvents.MainMenu.OnMainMenuOpen -= MainMenuOpen;
        //이동 이벤트 구독
        InputEvents.MainMenu.OnCancel -= ESC;
        //Submit 이벤트 구독
        InputEvents.MainMenu.OnSubmit -= Enter;
        //Interact 이벤트 구독
        InputEvents.MainMenu.OnInteract -= E;
    }

    //SettingOpen 이벤트 구독
    void MainMenuOpen(InputContext sourceInputContext)
    {
        mainMenu.MainMenuOpen(sourceInputContext);
    }

    //Cancel 이벤트 구독
    void ESC(bool esc)
    {
        if (esc)
            mainMenu.ESC(esc);
    }
    //Submit 이벤트 구독
    void Enter(bool enter)
    {
        if (enter)
            mainMenu.Enter(enter);
    }
    //Interact 이벤트 구독
    void E(bool e)
    {
        if (e)
            mainMenu.E(e);
    }

}
