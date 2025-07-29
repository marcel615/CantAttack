using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TutorialEvents
{
    //Tutorial UI�� Open�ϱ� ���� �̺�Ʈ
    public static event Action<string> OnTutorialOpen;

    //Tutorial UI�� Close�ϱ� ���� �̺�Ʈ
    public static event Action OnTutorialClose;


    //Tutorial UI�� Open�ϱ� ���� �̺�Ʈ
    public static void InvokeTutorialOpen(string message)
    {
        OnTutorialOpen?.Invoke(message);
    }
    //Tutorial UI�� Close�ϱ� ���� �̺�Ʈ
    public static void InvokeTutorialClose()
    {
        OnTutorialClose?.Invoke();
    }

}
