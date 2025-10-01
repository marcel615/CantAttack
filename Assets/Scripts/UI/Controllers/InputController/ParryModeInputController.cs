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
        //Tab 이벤트 구독
        InputEvents.ParryMode.OnTab += Tab;
    }
    private void OnDisable()
    {
        //ParryModeOpen 이벤트 구독
        InputEvents.ParryMode.OnParryModeOpen -= ParryModeOpen;
        //ParryModeClose 이벤트 구독
        InputEvents.ParryMode.OnParryModeClose -= ParryModeClose;
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

    //Tab 이벤트 구독
    void Tab(bool tab)
    {
        if (tab)
            parryMode.Tab(tab);
    }



}
