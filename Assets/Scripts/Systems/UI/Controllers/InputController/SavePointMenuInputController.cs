using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointMenuInputController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private SavePointMenu savePointMenu;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (savePointMenu == null) savePointMenu = transform.Find("UICanvas/SavePointMenuPanel")?.GetComponent<SavePointMenu>();

    }

    //이벤트 구독
    private void OnEnable()
    {
        //SavePointMenuOpen 이벤트 구독
        InputEvents.SavePointMenu.OnSavePointMenuOpen += SavePointMenuOpen;
        //SavePointMenuClose 이벤트 구독
        InputEvents.SavePointMenu.OnSavePointMenuClose += SavePointMenuClose;
        //Cancel 이벤트 구독
        InputEvents.SavePointMenu.OnCancel += ESC;
        //Submit 이벤트 구독
        InputEvents.SavePointMenu.OnSubmit += Enter;
        //Interact 이벤트 구독
        InputEvents.SavePointMenu.OnInteract += E;
    }
    private void OnDisable()
    {
        //SavePointMenuOpen 이벤트 구독
        InputEvents.SavePointMenu.OnSavePointMenuOpen -= SavePointMenuOpen;
        //SavePointMenuClose 이벤트 구독
        InputEvents.SavePointMenu.OnSavePointMenuClose -= SavePointMenuClose;
        //Cancel 이벤트 구독
        InputEvents.SavePointMenu.OnCancel -= ESC;
        //Submit 이벤트 구독
        InputEvents.SavePointMenu.OnSubmit -= Enter;
        //Interact 이벤트 구독
        InputEvents.SavePointMenu.OnInteract -= E;
    }

    //SavePointMenuOpen 이벤트 구독
    void SavePointMenuOpen(InputContext sourceInputContext)
    {
        savePointMenu.SavePointMenuOpen(sourceInputContext);
    }
    //SavePointMenuClose 이벤트 구독
    void SavePointMenuClose(InputContext sourceInputContext)
    {
        savePointMenu.SavePointMenuClose(sourceInputContext);
    }

    //Cancel 이벤트 구독
    void ESC(bool esc)
    {
        if (esc)
            savePointMenu.ESC(esc);
    }
    //Submit 이벤트 구독
    void Enter(bool enter)
    {
        if (enter)
            savePointMenu.Enter(enter);
    }
    //Interact 이벤트 구독
    void E(bool e)
    {
        if (e)
            savePointMenu.E(e);
    }

}
