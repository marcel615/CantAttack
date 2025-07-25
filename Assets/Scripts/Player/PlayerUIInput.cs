using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIInput : MonoBehaviour
{
    //���ؽ�Ʈ enum ����
    public InputContext thisContext = InputContext.Player;

    public void ESC(bool esc)
    {
        InputEvents.InvokeContextUpdate(thisContext, false);
        InputEvents.InvokeContextUpdate(InputContext.SystemMenu, true);
        InputEvents.SystemMenu.InvokeSystemMenuOpen();
    }
}
