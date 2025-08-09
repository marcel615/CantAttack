using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingEvents
{
    //SaveSlotLoading UI�� Open�ϱ� ���� �̺�Ʈ
    public static event Action<SceneChangeType, float, string, int> OnSaveSlotLoadingOpen;
    //SaveSlotLoading UI�� Close�ϱ� ���� �̺�Ʈ
    public static event Action OnSaveSlotLoadingClose;

    //PortalLoading UI�� Open�ϱ� ���� �̺�Ʈ
    public static event Action<float, string> OnPortalLoadingOpen;
    //PortalLoading UI�� Close�ϱ� ���� �̺�Ʈ
    public static event Action OnPortalLoadingClose;

    //RespawnLoading UI�� Open�ϱ� ���� �̺�Ʈ
    public static event Action<float, string> OnRespawnLoadingOpen;
    //RespawnLoading UI�� Close�ϱ� ���� �̺�Ʈ
    public static event Action OnRespawnLoadingClose;

    //SaveExitLoading UI�� Open�ϱ� ���� �̺�Ʈ
    public static event Action<float, string> OnSaveExitLoadingOpen;
    //SaveExitLoading UI�� Close�ϱ� ���� �̺�Ʈ
    public static event Action OnSaveExitLoadingClose;


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

    //RespawnLoading UI�� Open�ϱ� ���� �̺�Ʈ
    public static void InvokeRespawnLoadingOpen(float fadeTime, string targetScene)
    {
        OnRespawnLoadingOpen?.Invoke(fadeTime, targetScene);
    }
    //RespawnLoading UI�� Close�ϱ� ���� �̺�Ʈ
    public static void InvokeRespawnLoadingClose()
    {
        OnRespawnLoadingClose?.Invoke();
    }

    //SaveExitLoading UI�� Open�ϱ� ���� �̺�Ʈ
    public static void InvokeOnSaveExitLoadingOpen(float fadeTime, string targetScene)
    {
        OnSaveExitLoadingOpen?.Invoke(fadeTime, targetScene);
    }
    //SaveExitLoading UI�� Close�ϱ� ���� �̺�Ʈ
    public static void InvokeOnSaveExitLoadingClose()
    {
        OnSaveExitLoadingClose?.Invoke();
    }

}
