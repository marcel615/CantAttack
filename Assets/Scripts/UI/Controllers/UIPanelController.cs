using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIPanelController
{
    //UI���� �� Panel�� �� �� ���Ǵ� �޼ҵ�
    public static void OpenPanel(Stack<GameObject> panelStack, ref GameObject currentPanel, GameObject newPanel, GameObject rootPanel)
    {
        if (currentPanel != null)
        {
            panelStack.Push(currentPanel);
            currentPanel.SetActive(false);
        }
        else
        {
            rootPanel.SetActive(true);
        }
        currentPanel = newPanel;
        currentPanel.SetActive(true);
        InputEvents.InvokeSelectFirstSelectable(currentPanel);

    }
    //UI���� ���� Panel�� �� �� ���Ǵ� �޼ҵ�
    public static void Back(Stack<GameObject> panelStack, ref GameObject currentPanel)
    {
        currentPanel.SetActive(false);
        currentPanel = panelStack.Pop();
        currentPanel.SetActive(true);
        InputEvents.InvokeSelectFirstSelectable(currentPanel);
    }
    //UI���� ���� Panel�� ���� �� ���Ǵ� �޼ҵ� 
    public static void Close(ref GameObject currentPanel, GameObject rootPanel, InputContext thisContext)
    {
        currentPanel.SetActive(false);
        currentPanel = null;
        rootPanel.SetActive(false);
        InputEvents.InvokeContextUpdate(thisContext, false);
    }

}
