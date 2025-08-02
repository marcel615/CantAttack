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

    //Tutorial 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //출력할 메시지들
    string message;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (TextArea == null) TextArea = transform.Find("TutorialMessage")?.GetComponent<TextMeshProUGUI>();
    }

    //어디선가 Tutorial 패널을 열었을 때
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
