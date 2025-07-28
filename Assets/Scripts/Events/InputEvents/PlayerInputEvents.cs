using System;

public class PlayerInputEvents
{
    //Horizontal 이동 이벤트
    public event Action<float> OnMove;
    //Jump 이벤트
    public event Action<bool> OnJump;
    //JumpHold 이벤트
    public event Action<bool> OnJumpHold;
    //대쉬 이벤트
    public event Action<bool> OnDash;
    //패링 이벤트
    public event Action<bool> OnParry;
    //ESC 이벤트 (시스템 메뉴 열기)
    public event Action<bool> OnCancel;
    //E 이벤트 (인터랙션)
    public event Action<bool> OnInteract;

    //Horizontal 이동 이벤트
    public void InvokeMove(float h)
    {
        OnMove?.Invoke(h);
    }
    //Jump 이벤트
    public void InvokeJump(bool j)
    {
        OnJump?.Invoke(j);
    }
    //JumpHold 이벤트
    public void InvokeJumpHold(bool j_Hold)
    {
        OnJumpHold?.Invoke(j_Hold);
    }
    //대쉬 이벤트
    public void InvokeDash(bool d)
    {
        OnDash?.Invoke(d);
    }
    //패링 이벤트
    public void InvokeParry(bool p)
    {
        OnParry?.Invoke(p);
    }
    //ESC 이벤트 (시스템 메뉴 열기)
    public void InvokeCancel(bool esc)
    {
        OnCancel?.Invoke(esc);
    }
    //E 이벤트 (인터랙션)
    public void InvokeInteract(bool e)
    {
        OnInteract?.Invoke(e);
    }
}
