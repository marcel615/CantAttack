using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParryModeInputEvents
{
    //ParryMode UI에 접근하기 위한 이벤트
    public event Action<InputContext> OnParryModeOpen;
    //ParryMode UI Close하기 위한 이벤트
    public event Action<InputContext> OnParryModeClose;

    //Tab 이벤트 (패널 닫기)
    public event Action<bool> OnTab;


    //ParryMode UI에 접근하기 위한 이벤트
    public void InvokeParryModeOpen(InputContext sourceInputContext)
    {
        OnParryModeOpen?.Invoke(sourceInputContext);
    }
    //ParryMode UI Close하기 위한 이벤트
    public void InvokeParryModeClose(InputContext sourceInputContext)
    {
        OnParryModeClose?.Invoke(sourceInputContext);
    }

    //Tab 이벤트 (패널 닫기)
    public void InvokeTab(bool tab)
    {
        OnTab?.Invoke(tab);
    }

}
