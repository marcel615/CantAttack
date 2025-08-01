using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeEvents
{
    //Fade UI�� Open�ϱ� ���� �̺�Ʈ
    public static event Action<float, FadeDirection> OnFadeOpen;
    //Fade UI�� Close�ϱ� ���� �̺�Ʈ
    public static event Action OnFadeClose;



    //Fade UI�� Open�ϱ� ���� �̺�Ʈ
    public static void InvokeFadeOpen(float fadeTime, FadeDirection fadeDirection)
    {
        OnFadeOpen?.Invoke(fadeTime, fadeDirection);
    }
    //Fade UI�� Close�ϱ� ���� �̺�Ʈ
    public static void InvokeFadeClose()
    {
        OnFadeClose?.Invoke();
    }

}
