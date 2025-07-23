using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class InputEvents : MonoBehaviour
{
    //Horizontal �̵� �̺�Ʈ
    public static event Action<float> OnMove;
    //Jump �̺�Ʈ
    public static event Action<bool> OnJump;
    //JumpHold �̺�Ʈ
    public static event Action<bool> OnJumpHold;
    //�뽬 �̺�Ʈ
    public static event Action<bool> OnDash;
    //�и� �̺�Ʈ
    public static event Action<bool> OnParry;

    //Horizontal �̵� �̺�Ʈ
    public static void InvokeMove(float h)
    {
        OnMove?.Invoke(h);
    }
    //Jump �̺�Ʈ
    public static void InvokeJump(bool j)
    {
        OnJump?.Invoke(j);
    }
    //JumpHold �̺�Ʈ
    public static void InvokeJumpHold(bool j_Hold)
    {
        OnJumpHold?.Invoke(j_Hold);
    }
    //�뽬 �̺�Ʈ
    public static void InvokeDash(bool d)
    {
        OnDash?.Invoke(d);
    }
    //�и� �̺�Ʈ
    public static void InvokeParry(bool p)
    {
        OnParry?.Invoke(p);
    }

}