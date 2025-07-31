using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeEvents
{
    //Fade UI를 Open하기 위한 이벤트
    public static event Action<string, FadeDirection> OnFadeOpen;
    //Fade UI를 Close하기 위한 이벤트
    public static event Action OnFadeClose;

    //LoadingScene의 Fade UI를 Open하기 위한 이벤트
    public static event Action<float, FadeDirection> OnLoadingSceneFadeOpen;
    //LoadingScene의 Fade UI를 Close하기 위한 이벤트
    public static event Action OnLoadingSceneFadeClose;


    //Fade UI를 Open하기 위한 이벤트
    public static void InvokeFadeOpen(string targetScene, FadeDirection fadeDirection)
    {
        OnFadeOpen?.Invoke(targetScene, fadeDirection);
    }
    //Fade UI를 Close하기 위한 이벤트
    public static void InvokeFadeClose()
    {
        OnFadeClose?.Invoke();
    }

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
}
