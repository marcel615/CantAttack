using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraEvents
{
    //Camera Follow ������ ��
    public static event Action OnCameraFollowReset;

    //Camera Follow ������ ��
    public static void InvokeCameraFollowReset()
    {
        OnCameraFollowReset?.Invoke();
    }

}
