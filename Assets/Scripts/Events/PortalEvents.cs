using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PortalEvents
{
    //Portal�� ������ ��
    public static event Action<string, string, string, PortalWalkDirection> OnPortalEnter;
    public static event Action OnPortalEnter_NoParameter;


    //Portal�� ������ ��
    public static void InvokePortalEnter(string enterPortalID, string targetScene, string targetPortalID, PortalWalkDirection walkDir)
    {
        OnPortalEnter?.Invoke(enterPortalID, targetScene, targetPortalID, walkDir);
        OnPortalEnter_NoParameter?.Invoke();
    }

}
