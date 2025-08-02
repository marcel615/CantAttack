using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingSceneEvents
{
    //LoadingScene의 Fade UI를 Open하기 위한 이벤트
    public static event Action<float, FadeDirection> OnLoadingSceneFadeOpen;
    //LoadingScene의 Fade UI를 Close하기 위한 이벤트
    public static event Action OnLoadingSceneFadeClose;

    //SaveSlotLoading UI를 Open하기 위한 이벤트
    public static event Action<SceneChangeType, float, string, int> OnSaveSlotLoadingOpen;
    //SaveSlotLoading UI를 Close하기 위한 이벤트
    public static event Action OnSaveSlotLoadingClose;

    //PortalLoading UI를 Open하기 위한 이벤트
    public static event Action<float, string> OnPortalLoadingOpen;
    //PortalLoading UI를 Close하기 위한 이벤트
    public static event Action OnPortalLoadingClose;


    //LoadingScene의 Fade UI를 Open하기 위한 이벤트
    public static void InvokeLoadingSceneFadeOpen(float fadeTime, FadeDirection fadeDirection)
    {
        OnLoadingSceneFadeOpen?.Invoke(fadeTime, fadeDirection);
    }
    //LoadingScene의 Fade UI를 Close하기 위한 이벤트
    public static void InvokeLoadingSceneFadeClose()
    {
        OnLoadingSceneFadeClose?.Invoke();
    }

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

}
