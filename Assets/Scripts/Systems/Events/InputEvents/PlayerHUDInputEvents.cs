using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDInputEvents
{
    //PlayerHUD UI를 보이게 할지 조절하기 위한 이벤트
    public event Action<bool> OnPlayerHUDVisible;


    //PlayerHUD UI를 보이게 할지 조절하기 위한 이벤트
    public void InvokePlayerHUDVisible(bool visible)
    {
        OnPlayerHUDVisible?.Invoke(visible);
    }

}
