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

    //ESC 이벤트 (나가기, 메뉴 열기)
    public event Action<bool> OnCancel;
    //Enter 이벤트 (선택)
    public event Action<bool> OnSubmit;
    //E 이벤트 (획득, 선택)
    public event Action<bool> OnInteract;
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

    //ESC 이벤트 (나가기, 메뉴 열기)
    public void InvokeCancel(bool esc)
    {
        OnCancel?.Invoke(esc);
    }
    //Enter 이벤트 (선택)
    public void InvokeSubmit(bool enter)
    {
        OnSubmit?.Invoke(enter);
    }
    //E 이벤트 (획득, 선택)
    public void InvokeInteract(bool e)
    {
        OnInteract?.Invoke(e);
    }
    //Tab 이벤트 (패널 닫기)
    public void InvokeTab(bool tab)
    {
        OnTab?.Invoke(tab);
    }

}
