using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemMenuInputController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private SystemMenu systemMenu;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (systemMenu == null) systemMenu = transform.Find("UICanvas/SystemMenuPanel")?.GetComponent<SystemMenu>();

    }

    //이벤트 구독
    private void OnEnable()
    {
        //Cancel 이벤트 구독
        InputEvents.SystemMenu.OnCancel += ESC;
        //Submit 이벤트 구독
        InputEvents.SystemMenu.OnSubmit += Enter;
        //Interact 이벤트 구독
        InputEvents.SystemMenu.OnInteract += E;
    }
    private void OnDisable()
    {
        //이동 이벤트 구독
        InputEvents.SystemMenu.OnCancel -= ESC;
        //Submit 이벤트 구독
        InputEvents.SystemMenu.OnSubmit -= Enter;
        //Interact 이벤트 구독
        InputEvents.SystemMenu.OnInteract -= E;
    }

    //Cancel 이벤트 구독
    void ESC(bool esc)
    {
        if (esc) 
            systemMenu.ESC(esc);
    }
    //Submit 이벤트 구독
    void Enter(bool enter)
    {
        if(enter)
            systemMenu.Enter(enter);
    }
    //Interact 이벤트 구독
    void E(bool e)
    {
        if (e)
            systemMenu.E(e);
    }

}
