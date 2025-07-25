using System;

public class PlayerInputEvents
{
    //Horizontal �̵� �̺�Ʈ
    public event Action<float> OnMove;
    //Jump �̺�Ʈ
    public event Action<bool> OnJump;
    //JumpHold �̺�Ʈ
    public event Action<bool> OnJumpHold;
    //�뽬 �̺�Ʈ
    public event Action<bool> OnDash;
    //�и� �̺�Ʈ
    public event Action<bool> OnParry;
    //ESC �̺�Ʈ (�ý��� �޴� ����)
    public event Action<bool> OnCancel;

    //Horizontal �̵� �̺�Ʈ
    public void InvokeMove(float h)
    {
        OnMove?.Invoke(h);
    }
    //Jump �̺�Ʈ
    public void InvokeJump(bool j)
    {
        OnJump?.Invoke(j);
    }
    //JumpHold �̺�Ʈ
    public void InvokeJumpHold(bool j_Hold)
    {
        OnJumpHold?.Invoke(j_Hold);
    }
    //�뽬 �̺�Ʈ
    public void InvokeDash(bool d)
    {
        OnDash?.Invoke(d);
    }
    //�и� �̺�Ʈ
    public void InvokeParry(bool p)
    {
        OnParry?.Invoke(p);
    }
    //ESC �̺�Ʈ (�ý��� �޴� ����)
    public void InvokeCancel(bool esc)
    {
        OnCancel?.Invoke(esc);
    }
}
