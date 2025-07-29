using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessageManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static TutorialMessageManager Instance;

    //등록된 튜토리얼 메시지들
    public List<TutorialMessageSO> tutorialMessages;
    //메시지들로 딕셔너리 만들기
    private Dictionary<string, string> messageDict;



    private void Awake()
    {
        // 기존 인스턴스가 존재할 때 && 지금 새로운 인스턴스가 생성되려고 할 때
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //중복되지 않도록 지금 새롭게 생성되는 놈은 파괴시킨다
            return;
        }
        // 인스턴스 처음 할당
        Instance = this;
        DontDestroyOnLoad(gameObject);

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
