using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PortalEvents
{
    //Portal�� ������ ��
    public static event Action OnPortalEnter;


    //Portal�� ������ ��
    public static void InvokePortalEnter()
    {
        OnPortalEnter?.Invoke();
    }

}
