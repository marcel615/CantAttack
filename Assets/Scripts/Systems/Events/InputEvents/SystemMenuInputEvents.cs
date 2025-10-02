using System;

public class SystemMenuInputEvents
{
    //SystemMenu UI에 접근하기 위한 이벤트
    public event Action<InputContext> OnSystemMenuOpen;
    //SystemMenu UI를 닫기 위한 이벤트
    public event Action<InputContext> OnSystemMenuClose;

    //ESC 이벤트 (나가기, 메뉴 열기)
    public event Action<bool> OnCancel;
    //Enter 이벤트 (선택)
    public event Action<bool> OnSubmit;
    //E 이벤트 (획득, 선택)
    public event Action<bool> OnInteract;


    //SystemMenu UI에 접근하기 위한 이벤트
    public void InvokeSystemMenuOpen(InputContext sourceInputContext)
    {
        OnSystemMenuOpen?.Invoke(sourceInputContext);
    }
    //SystemMenu UI를 닫기 위한 이벤트
    public void InvokeSystemMenuClose(InputContext sourceInputContext)
    {
        OnSystemMenuClose?.Invoke(sourceInputContext);
    }

    //ESC 이벤트 (나가기, 메뉴 열기)
    public void InvokeCancel(bool esc)
    {
        OnCancel?.Invoke(esc);
    }
    //Enter 이벤트 (선택)
    public void InvokeSubmit(bool enter)
    {
        OnSubmit?.Invoke(enter);
    }
    //E 이벤트 (획득, 선택)
    public void InvokeInteract(bool e)
    {
        OnInteract?.Invoke(e);
    }

}
