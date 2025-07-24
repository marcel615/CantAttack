using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class InputEvents : MonoBehaviour
{
    //InputManager�� Context ���� �̺�Ʈ
    public static event Action<InputContext, bool> OnContextUpdate;


    /// <Player>
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
    /// </Playter>
    
    /// <SystemMenu>
    //ESC �̺�Ʈ (������, �޴� ����)
    public static event Action<bool> OnCancel;
    //Enter �̺�Ʈ (����)
    public static event Action<bool> OnSubmit;
    //E �̺�Ʈ (ȹ��, ����)
    public static event Action<bool> OnInteract;
    /// </SystemMenu>

    /// <?>
    //R �̺�Ʈ (ȸ�� ������ ���)
    public static event Action<bool> OnUseHealItem;
    /// </?>


    //InputManager�� Context ���� �̺�Ʈ
    public static void InvokeContextUpdate(InputContext context, bool state)
    {
        OnContextUpdate?.Invoke(context, state);
    }

    /// <Player>
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
    /// </Playter>

    /// <SystemMenu>
    //ESC �̺�Ʈ (������, �޴� ����)
    public static void InvokeCancel(bool esc)
    {
        OnCancel?.Invoke(esc);
    }
    //Enter �̺�Ʈ (����)
    public static void InvokeSubmit(bool enter)
    {
        OnSubmit?.Invoke(enter);
    }
    //E �̺�Ʈ (ȹ��, ����)
    public static void InvokeInteract(bool e)
    {
        OnInteract?.Invoke(e);
    }
    /// </SystemMenu>

    /// <Item>
    //R �̺�Ʈ (ȸ�� ������ ���)
    public static void InvokeUseHealItem(bool r)
    {
        OnUseHealItem?.Invoke(r);
    }
    /// </Item>

}