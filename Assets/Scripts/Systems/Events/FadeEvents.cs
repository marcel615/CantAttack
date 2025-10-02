using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeEvents
{
    //Fade UI를 Open하기 위한 이벤트
    public static event Action<float, FadeDirection> OnFadeOpen;
    //Fade UI를 Close하기 위한 이벤트
    public static event Action OnFadeClose;



    //Fade UI를 Open하기 위한 이벤트
    public static void InvokeFadeOpen(float fadeTime, FadeDirection fadeDirection)
    {
        OnFadeOpen?.Invoke(fadeTime, fadeDirection);
    }
    //Fade UI를 Close하기 위한 이벤트
    public static void InvokeFadeClose()
    {
        OnFadeClose?.Invoke();
    }

}
