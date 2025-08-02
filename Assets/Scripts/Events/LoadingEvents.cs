using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingEvents
{
    //SaveSlotLoading UI를 Open하기 위한 이벤트
    public static event Action<SceneChangeType, float, string, int> OnSaveSlotLoadingOpen;
    //SaveSlotLoading UI를 Close하기 위한 이벤트
    public static event Action OnSaveSlotLoadingClose;

    //PortalLoading UI를 Open하기 위한 이벤트
    public static event Action<float, string> OnPortalLoadingOpen;
    //PortalLoading UI를 Close하기 위한 이벤트
    public static event Action OnPortalLoadingClose;

    //SaveExitLoading UI를 Open하기 위한 이벤트
    public static event Action<float, string> OnSaveExitLoadingOpen;
    //SaveExitLoading UI를 Close하기 위한 이벤트
    public static event Action OnSaveExitLoadingClose;


    //SaveSlotLoading UI를 Open하기 위한 이벤트
    public static void InvokeSaveSlotLoadingOpen(SceneChangeType sceneChangeType, float fadeTime, string targetScene, int slotNum)
    {
        OnSaveSlotLoadingOpen?.Invoke(sceneChangeType, fadeTime, targetScene, slotNum);
    }
    //SaveSlotLoading UI를 Close하기 위한 이벤트
    public static void InvokeSaveSlotLoadingClose()
    {
        OnSaveSlotLoadingClose?.Invoke();
    }

    //PortalLoading UI를 Open하기 위한 이벤트
    public static void InvokePortalLoadingOpen(float fadeTime, string targetScene)
    {
        OnPortalLoadingOpen?.Invoke(fadeTime, targetScene);
    }
    //PortalLoading UI를 Close하기 위한 이벤트
    public static void InvokePortalLoadingClose()
    {
        OnPortalLoadingClose?.Invoke();
    }

    //SaveExitLoading UI를 Open하기 위한 이벤트
    public static void InvokeOnSaveExitLoadingOpen(float fadeTime, string targetScene)
    {
        OnSaveExitLoadingOpen?.Invoke(fadeTime, targetScene);
    }
    //SaveExitLoading UI를 Close하기 위한 이벤트
    public static void InvokeOnSaveExitLoadingClose()
    {
        OnSaveExitLoadingClose?.Invoke();
    }

}
