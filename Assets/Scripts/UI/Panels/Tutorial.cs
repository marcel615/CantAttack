using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private TextMeshProUGUI TextArea;

    //컨텍스트 enum 정보
    public InputContext thisContext = InputContext.Whatever;

    //Dialogue 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //출력할 메시지들
    string message;


    //어디선가 Tutorial 패널을 열었을 때
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
