using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessageManager : MonoBehaviour
{
    //등록된 튜토리얼 메시지들
    public List<TutorialMessageSO> tutorialMessages;
    //메시지들로 딕셔너리 만들기
    private Dictionary<string, string> messageDict;



    private void Awake()
    {
        messageDict = new Dictionary<string, string>();
        SetMessageDic();
    }
    void SetMessageDic()
    {
        foreach (var msg in tutorialMessages)
        {
            if (!messageDict.ContainsKey(msg.messageID))
                messageDict.Add(msg.messageID, msg.messageKor);
        }
    }
    public void ShowMessage(TutorialMessageSO msgSO)
    {
        string Message = null;

        if (messageDict.ContainsKey(msgSO.messageID))
        {
            Message = msgSO.messageKor;
        }
        //Tutorial 열고 메시지도 전달하도록 구현
        TutorialEvents.InvokeTutorialOpen(Message);
    }
    public void HideMessage(TutorialMessageSO msgSO)
    {

        //Tutorial 닫도록 구현
        TutorialEvents.InvokeTutorialClose();
    }

}
