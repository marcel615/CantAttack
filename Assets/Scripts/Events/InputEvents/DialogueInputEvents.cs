using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInputEvents
{
    //Dialogue UI에 접근하기 위한 이벤트
    public event Action<InputContext, List<string>> OnDialogueOpen;
    //Dialogue UI가 닫히면 발행하는 이벤트
    public event Action OnDialogueClose;


    //ESC 이벤트 (나가기)
    public event Action<bool> OnCancel;
    //Enter 이벤트 (선택)
    public event Action<bool> OnSubmit;
    //E 이벤트 (획득, 선택)
    public event Action<bool> OnInteract;



    //Dialogue UI에 접근하기 위한 이벤트
    public void InvokeDialogueOpen(InputContext sourceInputContext, List<String> messages)
    {
        OnDialogueOpen?.Invoke(sourceInputContext, messages);
    }
    //Dialogue UI가 닫히면 발행하는 이벤트
    public void InvokeDialogueClose()
    {
        OnDialogueClose?.Invoke();
    }


    //ESC 이벤트 (나가기)
    public void InvokeCancel(bool esc)
    {
        OnCancel?.Invoke(esc);
    }
    //Enter 이벤트 (대사 넘기기)
    public void InvokeSubmit(bool enter)
    {
        OnSubmit?.Invoke(enter);
    }
    //E 이벤트 (대사 넘기기)
    public void InvokeInteract(bool e)
    {
        OnInteract?.Invoke(e);
    }

}
