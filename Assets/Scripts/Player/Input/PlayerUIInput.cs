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

        //이 때 튜토리얼이 열려있었다면 닫도록 해야 하기 때문에 실행
        TutorialEvents.InvokeTutorialClose();

    }
}