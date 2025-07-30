using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PortalEvents
{
    //Portal에 들어왔을 때
    public static event Action OnPortalEnter;


    //Portal에 들어왔을 때
    public static void InvokePortalEnter()
    {
        OnPortalEnter?.Invoke();
    }

}
