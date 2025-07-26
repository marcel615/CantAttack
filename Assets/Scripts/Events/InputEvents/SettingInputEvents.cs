using System;

public class SettingInputEvents
{
    //Setting UI에 접근하기 위한 이벤트
    public event Action<InputContext> OnSettingOpen;


    //ESC 이벤트 (나가기)
    public event Action<bool> OnCancel;
    //Enter 이벤트 (선택)
    public event Action<bool> OnSubmit;
    //E 이벤트 (획득, 선택)
    public event Action<bool> OnInteract;



    //Setting UI에 접근하기 위한 이벤트
    public void InvokeSettingOpen(InputContext sourceInputContext)
    {
        OnSettingOpen?.Invoke(sourceInputContext);
    }


    //ESC 이벤트 (나가기)
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
