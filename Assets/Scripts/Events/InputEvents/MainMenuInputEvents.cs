using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInputEvents
{
    //MainMenu UI�� �����ϱ� ���� �̺�Ʈ
    public event Action<InputContext> OnMainMenuOpen;

    //ESC �̺�Ʈ (������, �޴� ����)
    public event Action<bool> OnCancel;
    //Enter �̺�Ʈ (����)
    public event Action<bool> OnSubmit;
    //E �̺�Ʈ (ȹ��, ����)
    public event Action<bool> OnInteract;


    //MainMenu UI�� �����ϱ� ���� �̺�Ʈ
    public void InvokeMainMenuOpen(InputContext sourceInputContext)
    {
        OnMainMenuOpen?.Invoke(sourceInputContext);
    }

    //ESC �̺�Ʈ (������, �޴� ����)
    public void InvokeCancel(bool esc)
    {
        OnCancel?.Invoke(esc);
    }
    //Enter �̺�Ʈ (����)
    public void InvokeSubmit(bool enter)
    {
        OnSubmit?.Invoke(enter);
    }
    //E �̺�Ʈ (ȹ��, ����)
    public void InvokeInteract(bool e)
    {
        OnInteract?.Invoke(e);
    }
}
