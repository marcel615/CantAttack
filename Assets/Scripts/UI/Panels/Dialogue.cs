using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

    //자식 오브젝트
    [SerializeField] private TextMeshProUGUI TextArea;


    //컨텍스트 enum 정보
    public InputContext thisContext = InputContext.Dialogue;
    public InputContext beforeContext;

    //Dialogue 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //출력할 메시지들
    List<string> messages = new List<string>();
    int messagesCount;
    int messagesMax;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (TextArea == null) TextArea = transform.Find("DialgueMessage")?.GetComponent<TextMeshProUGUI>();
    }

    //어디선가 Dialogue 패널을 열었을 때
    public void DialogueOpen(InputContext sourceInputContext, List<string> msg)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, gameObject);
        InputEvents.InvokeContextUpdate(thisContext, true);

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
            //뒤로가기
            UIPanelController.Back(panelStack, ref currentPanel);
        }
        else
        {
            //닫기
            UIPanelController.Close(ref currentPanel, gameObject);
            InputEvents.InvokeContextUpdate(thisContext, false);
            InputEvents.InvokeContextUpdate(InputContext.Player, true);
        }
    }
    //Enter랑 E 둘 다 대사 넘기기 작업 구현하기
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
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext, false);
        InputEvents.InvokeContextUpdate(InputContext.Player, true);
    }
    void CheckMessageMax()
    {
        //message 다 읽음
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
