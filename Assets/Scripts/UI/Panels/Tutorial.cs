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

    //Tutorial ���� ���� ����
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //����� �޽�����
    string message;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (TextArea == null) TextArea = transform.Find("TutorialMessage")?.GetComponent<TextMeshProUGUI>();
    }

    //��𼱰� Tutorial �г��� ������ ��
    public void TutorialOpen(string msg)
    {
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, gameObject);

        message = msg;
        StartTutorial();
    }
    public void TutorialClose()
    {
        if (currentPanel != null)
        {
            UIPanelController.Close(ref currentPanel, gameObject);
            //InputEvents.InvokeContextUpdate(thisContext, false);
            message = null;
        }
    }
    void StartTutorial()
    {
        TextArea.text = message;
    }

}
