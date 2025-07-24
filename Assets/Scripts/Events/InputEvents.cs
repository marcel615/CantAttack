using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class InputEvents : MonoBehaviour
{
    //InputManager의 Context 관리 이벤트
    public static event Action<InputContext, bool> OnContextUpdate;


    /// <Player>
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
    /// </Playter>
    
    /// <SystemMenu>
    //ESC 이벤트 (나가기, 메뉴 열기)
    public static event Action<bool> OnCancel;
    //Enter 이벤트 (선택)
    public static event Action<bool> OnSubmit;
    //E 이벤트 (획득, 선택)
    public static event Action<bool> OnInteract;
    /// </SystemMenu>

    /// <?>
    //R 이벤트 (회복 아이템 사용)
    public static event Action<bool> OnUseHealItem;
    /// </?>


    //InputManager의 Context 관리 이벤트
    public static void InvokeContextUpdate(InputContext context, bool state)
    {
        OnContextUpdate?.Invoke(context, state);
    }

    /// <Player>
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
    /// </Playter>

    /// <SystemMenu>
    //ESC 이벤트 (나가기, 메뉴 열기)
    public static void InvokeCancel(bool esc)
    {
        OnCancel?.Invoke(esc);
    }
    //Enter 이벤트 (선택)
    public static void InvokeSubmit(bool enter)
    {
        OnSubmit?.Invoke(enter);
    }
    //E 이벤트 (획득, 선택)
    public static void InvokeInteract(bool e)
    {
        OnInteract?.Invoke(e);
    }
    /// </SystemMenu>

    /// <Item>
    //R 이벤트 (회복 아이템 사용)
    public static void InvokeUseHealItem(bool r)
    {
        OnUseHealItem?.Invoke(r);
    }
    /// </Item>

}