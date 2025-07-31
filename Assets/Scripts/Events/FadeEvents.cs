using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeEvents
{
    //Fade UI�� Open�ϱ� ���� �̺�Ʈ
    public static event Action<string, FadeDirection> OnFadeOpen;
    //Fade UI�� Close�ϱ� ���� �̺�Ʈ
    public static event Action OnFadeClose;

    //LoadingScene�� Fade UI�� Open�ϱ� ���� �̺�Ʈ
    public static event Action<float, FadeDirection> OnLoadingSceneFadeOpen;
    //LoadingScene�� Fade UI�� Close�ϱ� ���� �̺�Ʈ
    public static event Action OnLoadingSceneFadeClose;


    //Fade UI�� Open�ϱ� ���� �̺�Ʈ
    public static void InvokeFadeOpen(string targetScene, FadeDirection fadeDirection)
    {
        OnFadeOpen?.Invoke(targetScene, fadeDirection);
    }
    //Fade UI�� Close�ϱ� ���� �̺�Ʈ
    public static void InvokeFadeClose()
    {
        OnFadeClose?.Invoke();
    }

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
}
