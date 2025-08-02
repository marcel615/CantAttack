using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingSceneEvents
{
    //LoadingScene�� Fade UI�� Open�ϱ� ���� �̺�Ʈ
    public static event Action<float, FadeDirection> OnLoadingSceneFadeOpen;
    //LoadingScene�� Fade UI�� Close�ϱ� ���� �̺�Ʈ
    public static event Action OnLoadingSceneFadeClose;

    //SaveSlotLoading UI�� Open�ϱ� ���� �̺�Ʈ
    public static event Action<SceneChangeType, float, string, int> OnSaveSlotLoadingOpen;
    //SaveSlotLoading UI�� Close�ϱ� ���� �̺�Ʈ
    public static event Action OnSaveSlotLoadingClose;

    //PortalLoading UI�� Open�ϱ� ���� �̺�Ʈ
    public static event Action<float, string> OnPortalLoadingOpen;
    //PortalLoading UI�� Close�ϱ� ���� �̺�Ʈ
    public static event Action OnPortalLoadingClose;


    //LoadingScene�� Fade UI�� Open�ϱ� ���� �̺�Ʈ
    public static void InvokeLoadingSceneFadeOpen(float fadeTime, FadeDirection fadeDirection)
    {
        OnLoadingSceneFadeOpen?.Invoke(fadeTime, fadeDirection);
    }
    //LoadingScene�� Fade UI�� Close�ϱ� ���� �̺�Ʈ
    public static void InvokeLoadingSceneFadeClose()
    {
        OnLoadingSceneFadeClose?.Invoke();
    }

    //SaveSlotLoading UI�� Open�ϱ� ���� �̺�Ʈ
    public static void InvokeSaveSlotLoadingOpen(SceneChangeType sceneChangeType, float fadeTime, string targetScene, int slotNum)
    {
        OnSaveSlotLoadingOpen?.Invoke(sceneChangeType, fadeTime, targetScene, slotNum);
    }
    //SaveSlotLoading UI�� Close�ϱ� ���� �̺�Ʈ
    public static void InvokeSaveSlotLoadingClose()
    {
        OnSaveSlotLoadingClose?.Invoke();
    }

    //PortalLoading UI�� Open�ϱ� ���� �̺�Ʈ
    public static void InvokePortalLoadingOpen(float fadeTime, string targetScene)
    {
        OnPortalLoadingOpen?.Invoke(fadeTime, targetScene);
    }
    //PortalLoading UI�� Close�ϱ� ���� �̺�Ʈ
    public static void InvokePortalLoadingClose()
    {
        OnPortalLoadingClose?.Invoke();
    }

}
