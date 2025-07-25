using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIPanelController
{
    //UI에서 새 Panel을 열 때 사용되는 메소드
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
    //UI에서 이전 Panel을 열 때 사용되는 메소드
    public static void Back(Stack<GameObject> panelStack, ref GameObject currentPanel)
    {
        currentPanel.SetActive(false);
        currentPanel = panelStack.Pop();
        currentPanel.SetActive(true);
        InputEvents.InvokeSelectFirstSelectable(currentPanel);
    }
    //UI에서 현재 Panel을 닫을 때 사용되는 메소드 
    public static void Close(ref GameObject currentPanel, GameObject rootPanel, InputContext thisContext)
    {
        currentPanel.SetActive(false);
        currentPanel = null;
        rootPanel.SetActive(false);
        InputEvents.InvokeContextUpdate(thisContext, false);
    }

}
