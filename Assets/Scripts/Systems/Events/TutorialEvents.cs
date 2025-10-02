using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TutorialEvents
{
    //Tutorial UI를 Open하기 위한 이벤트
    public static event Action<string> OnTutorialOpen;

    //Tutorial UI를 Close하기 위한 이벤트
    public static event Action OnTutorialClose;


    //Tutorial UI를 Open하기 위한 이벤트
    public static void InvokeTutorialOpen(string message)
    {
        OnTutorialOpen?.Invoke(message);
    }
    //Tutorial UI를 Close하기 위한 이벤트
    public static void InvokeTutorialClose()
    {
        OnTutorialClose?.Invoke();
    }

}
