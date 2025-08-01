using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraEvents
{
    //Camera Follow 리셋할 때
    public static event Action OnCameraFollowReset;

    //Camera Follow 리셋할 때
    public static void InvokeCameraFollowReset()
    {
        OnCameraFollowReset?.Invoke();
    }

}
