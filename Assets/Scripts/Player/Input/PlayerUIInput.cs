using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIInput : MonoBehaviour
{
    //���ؽ�Ʈ enum ����
    InputContext thisContext = InputContext.Player;

    public void ESC(bool esc)
    {
        //SystemMenu ����
        InputEvents.SystemMenu.InvokeSystemMenuOpen(thisContext);

        //�� �� Ʃ�丮���� �����־��ٸ� �ݵ��� �ؾ� �ϱ� ������ ����
        TutorialEvents.InvokeTutorialClose();

    }
}