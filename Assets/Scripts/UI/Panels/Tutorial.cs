using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private TextMeshProUGUI TextArea;

    //���ؽ�Ʈ enum ����
    public InputContext thisContext = InputContext.Whatever;

    //Dialogue ���� ���� ����
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //����� �޽�����
    string message;


    //��𼱰� Tutorial �г��� ������ ��
    public void TutorialOpen(string msg)
    {
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, gameObject);

        message = msg;
        StartDialogue();
    }
    void StartDialogue()
    {
        TextArea.text = message;
    }
    public void TutorialClose()
    {
        if (currentPanel == null)
        {
            Debug.Log("Test");
        }
        UIPanelController.Close(ref currentPanel, gameObject, thisContext);
        message = null;
    }

}
