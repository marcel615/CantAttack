using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

    //�ڽ� ������Ʈ
    [SerializeField] private TextMeshProUGUI TextArea;


    //���ؽ�Ʈ enum ����
    InputContext thisContext = InputContext.Dialogue;
    InputContext beforeContext;

    //Dialogue ���� ���� ����
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //����� �޽�����
    List<string> messages = new List<string>();
    int messagesCount;
    int messagesMax;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (TextArea == null) TextArea = transform.Find("DialgueMessage")?.GetComponent<TextMeshProUGUI>();
    }

    //��𼱰� Dialogue �г��� ������ ��
    public void DialogueOpen(InputContext sourceInputContext, List<string> msg)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, gameObject);
        InputEvents.InvokeContextUpdate(thisContext);

        messages = msg;
        messagesMax = messages.Count;
        messagesCount = 0;
        StartDialogue();
    }
    ///<Input>
    public void ESC(bool esc)
    {
        if (panelStack.Count > 0)
        {
            //�ڷΰ���
            UIPanelController.Back(panelStack, ref currentPanel);
        }
        else
        {
            //�ݱ�
            InputEvents.Dialogue.InvokeDialogueClose();
            UIPanelController.Close(ref currentPanel, gameObject);
            InputEvents.InvokeContextUpdate(InputContext.Player);
        }
    }
    //Enter�� E �� �� ��� �ѱ�� �۾� �����ϱ�
    public void Enter(bool enter)
    {
        CheckMessageMax();
    }
    public void E(bool e)
    {
        CheckMessageMax();
    }
    /// </Input>
    
    void StartDialogue()
    {
        TextArea.text = messages[messagesCount];
    }
    void EndDialogue()
    {
        InputEvents.Dialogue.InvokeDialogueClose();
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(InputContext.Player);
    }
    void CheckMessageMax()
    {
        //message �� ����
        if (messagesCount == (messagesMax - 1))
        {
            messagesCount = 0;
            messagesMax = 0;
            messages.Clear();

            EndDialogue();
        }
        else
        {
            messagesCount++;
            StartDialogue();
        }
    }

}
