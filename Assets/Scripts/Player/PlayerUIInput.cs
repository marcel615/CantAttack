using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIInput : MonoBehaviour
{
    //컨텍스트 enum 정보
    public InputContext thisContext = InputContext.Player;

    public void ESC(bool esc)
    {
        //Tutorial 닫도록 구현
        TutorialEvents.InvokeTutorialClose();
        InputEvents.InvokeContextUpdate(thisContext, false);

        //SystemMenu 오픈
        InputEvents.InvokeContextUpdate(InputContext.SystemMenu, true);
        InputEvents.SystemMenu.InvokeSystemMenuOpen(thisContext);

    }
}
