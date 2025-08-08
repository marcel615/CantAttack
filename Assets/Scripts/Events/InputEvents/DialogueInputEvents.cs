using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInputEvents
{
    //Dialogue UI�� �����ϱ� ���� �̺�Ʈ
    public event Action<InputContext, List<string>> OnDialogueOpen;
    //Dialogue UI�� ������ �����ϴ� �̺�Ʈ
    public event Action OnDialogueClose;


    //ESC �̺�Ʈ (������)
    public event Action<bool> OnCancel;
    //Enter �̺�Ʈ (����)
    public event Action<bool> OnSubmit;
    //E �̺�Ʈ (ȹ��, ����)
    public event Action<bool> OnInteract;



    //Dialogue UI�� �����ϱ� ���� �̺�Ʈ
    public void InvokeDialogueOpen(InputContext sourceInputContext, List<String> messages)
    {
        OnDialogueOpen?.Invoke(sourceInputContext, messages);
    }
    //Dialogue UI�� ������ �����ϴ� �̺�Ʈ
    public void InvokeDialogueClose()
    {
        OnDialogueClose?.Invoke();
    }


    //ESC �̺�Ʈ (������)
    public void InvokeCancel(bool esc)
    {
        OnCancel?.Invoke(esc);
    }
    //Enter �̺�Ʈ (��� �ѱ��)
    public void InvokeSubmit(bool enter)
    {
        OnSubmit?.Invoke(enter);
    }
    //E �̺�Ʈ (��� �ѱ��)
    public void InvokeInteract(bool e)
    {
        OnInteract?.Invoke(e);
    }

}
