using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIInput : MonoBehaviour
{
    //���ؽ�Ʈ enum ����
    public InputContext thisContext = InputContext.Player;

    public void ESC(bool esc)
    {
        //Tutorial �ݵ��� ����
        TutorialEvents.InvokeTutorialClose();
        InputEvents.InvokeContextUpdate(thisContext, false);

        //SystemMenu ����
        InputEvents.InvokeContextUpdate(InputContext.SystemMenu, true);
        InputEvents.SystemMenu.InvokeSystemMenuOpen(thisContext);

    }
}
