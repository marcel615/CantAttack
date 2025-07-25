using System;

public class SystemMenuInputEvents
{
    //SystemMenu UI�� �����ϱ� ���� �̺�Ʈ
    public event Action OnSystemMenuOpen;

    //ESC �̺�Ʈ (������, �޴� ����)
    public event Action<bool> OnCancel;
    //Enter �̺�Ʈ (����)
    public event Action<bool> OnSubmit;
    //E �̺�Ʈ (ȹ��, ����)
    public event Action<bool> OnInteract;


    //SystemMenu UI�� �����ϱ� ���� �̺�Ʈ
    public void InvokeSystemMenuOpen()
    {
        OnSystemMenuOpen?.Invoke();
    }

    //ESC �̺�Ʈ (������, �޴� ����)
    public void InvokeCancel(bool esc)
    {
        OnCancel?.Invoke(esc);
    }
    //Enter �̺�Ʈ (����)
    public void InvokeSubmit(bool enter)
    {
        OnSubmit?.Invoke(enter);
    }
    //E �̺�Ʈ (ȹ��, ����)
    public void InvokeInteract(bool e)
    {
        OnInteract?.Invoke(e);
    }

}
