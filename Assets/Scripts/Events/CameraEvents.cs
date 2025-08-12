using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraEvents
{
    //Camera Follow 리셋할 때
    public static event Action OnCameraFollowReset;
    //Camera Follow 변경할 때
    public static event Action<Transform> OnCameraFollowChange;
    //Camera Follow 플레이어로 변경할 때
    public static event Action OnCameraFollowPlayer;


    //Camera Follow 리셋할 때
    public static void InvokeCameraFollowReset()
    {
        OnCameraFollowReset?.Invoke();
    }
    //Camera Follow 변경할 때
    public static void InvokeCameraFollowChange(Transform transform)
    {
        OnCameraFollowChange?.Invoke(transform);
    }
    //Camera Follow 플레이어로 변경할 때
    public static void InvokeCameraFollowPlayer()
    {
        OnCameraFollowPlayer?.Invoke();
    }

}
