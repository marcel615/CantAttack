using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIInput : MonoBehaviour
{
    //컨텍스트 enum 정보
    InputContext thisContext = InputContext.Player;

    public void ESC(bool esc)
    {
        //SystemMenu 오픈
        InputEvents.SystemMenu.InvokeSystemMenuOpen(thisContext);
    }
}