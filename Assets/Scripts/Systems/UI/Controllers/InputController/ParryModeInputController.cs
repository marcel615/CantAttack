using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryModeInputController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private ParryMode parryMode;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (parryMode == null) parryMode = transform.Find("UICanvas/ParryModePanel")?.GetComponent<ParryMode>();

    }

    //이벤트 구독
    private void OnEnable()
    {
        //ParryModeOpen 이벤트 구독
        InputEvents.ParryMode.OnParryModeOpen += ParryModeOpen;
        //ParryModeClose 이벤트 구독
        InputEvents.ParryMode.OnParryModeClose += ParryModeClose;
        //Cancel 이벤트 구독
        InputEvents.ParryMode.OnCancel += ESC;
        //Submit 이벤트 구독
        InputEvents.ParryMode.OnSubmit += Enter;
        //Interact 이벤트 구독
        InputEvents.ParryMode.OnInteract += E;
        //Tab 이벤트 구독
        InputEvents.ParryMode.OnTab += Tab;
    }
    private void OnDisable()
    {
        //ParryModeOpen 이벤트 구독
        InputEvents.ParryMode.OnParryModeOpen -= ParryModeOpen;
        //ParryModeClose 이벤트 구독
        InputEvents.ParryMode.OnParryModeClose -= ParryModeClose;
        //Cancel 이벤트 구독
        InputEvents.ParryMode.OnCancel -= ESC;
        //Submit 이벤트 구독
        InputEvents.ParryMode.OnSubmit -= Enter;
        //Interact 이벤트 구독
        InputEvents.ParryMode.OnInteract -= E;
        //Tab 이벤트 구독
        InputEvents.ParryMode.OnTab -= Tab;
    }

    //ParryModeOpen 이벤트 구독
    void ParryModeOpen(InputContext sourceInputContext)
    {
        parryMode.ParryModeOpen(sourceInputContext);
    }
    //ParryModeClose 이벤트 구독
    void ParryModeClose(InputContext sourceInputContext)
    {
        parryMode.ParryModeClose(sourceInputContext);
    }

    //Cancel 이벤트 구독
    void ESC(bool esc)
    {
        if (esc)
            parryMode.ESC(esc);
    }
    //Submit 이벤트 구독
    void Enter(bool enter)
    {
        if (enter)
            parryMode.Enter(enter);
    }
    //Interact 이벤트 구독
    void E(bool e)
    {
        if (e)
            parryMode.E(e);
    }
    //Tab 이벤트 구독
    void Tab(bool tab)
    {
        if (tab)
            parryMode.Tab(tab);
    }



}
