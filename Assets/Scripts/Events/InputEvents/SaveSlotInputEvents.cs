using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotInputEvents
{
    //SaveSlot UI에 접근하기 위한 이벤트
    public event Action<InputContext> OnSaveSlotOpen;
    //SaveSlot UI 닫는 이벤트
    public event Action<InputContext> OnSaveSlotClose;

    //ESC 이벤트 (나가기, 메뉴 열기)
    public event Action<bool> OnCancel;
    //Enter 이벤트 (선택)
    public event Action<bool> OnSubmit;
    //E 이벤트 (획득, 선택)
    public event Action<bool> OnInteract;


    //SaveSlot UI에 접근하기 위한 이벤트
    public void InvokeSaveSlotOpen(InputContext sourceInputContext)
    {
        OnSaveSlotOpen?.Invoke(sourceInputContext);
    }
    //SaveSlot UI 닫는 이벤트
    public void InvokeSaveSlotClose(InputContext sourceInputContext)
    {
        OnSaveSlotClose?.Invoke(sourceInputContext);
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

}
