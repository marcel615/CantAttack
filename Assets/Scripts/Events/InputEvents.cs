using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class InputEvents : MonoBehaviour
{
    //Horizontal 이동 이벤트
    public static event Action<float> OnMove;
    //Jump 이벤트
    public static event Action<bool> OnJump;
    //JumpHold 이벤트
    public static event Action<bool> OnJumpHold;
    //대쉬 이벤트
    public static event Action<bool> OnDash;
    //패링 이벤트
    public static event Action<bool> OnParry;

    //Horizontal 이동 이벤트
    public static void InvokeMove(float h)
    {
        OnMove?.Invoke(h);
    }
    //Jump 이벤트
    public static void InvokeJump(bool j)
    {
        OnJump?.Invoke(j);
    }
    //JumpHold 이벤트
    public static void InvokeJumpHold(bool j_Hold)
    {
        OnJumpHold?.Invoke(j_Hold);
    }
    //대쉬 이벤트
    public static void InvokeDash(bool d)
    {
        OnDash?.Invoke(d);
    }
    //패링 이벤트
    public static void InvokeParry(bool p)
    {
        OnParry?.Invoke(p);
    }

}