using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMessageManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static NPCMessageManager Instance;

    //등록된 NPC 메시지들
    public List<NPCMessageSO> NPCMessages;
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
        foreach (var msg in NPCMessages)
        {
            if (!messageDict.ContainsKey(msg.messageID))
                messageDict.Add(msg.messageID, msg.messageKor);
        }
    }
    public void ShowMessage(List<NPCMessageSO> msgSOs)
    {
        List<string> Messages = new List<string>();

        foreach (var msg in msgSOs)
        {
            if (messageDict.ContainsKey(msg.messageID))
            {
                Messages.Add(msg.messageKor);
            }
        }
        //Dialogue 열고 메시지리스트도 전달하도록 구현
        InputEvents.Dialogue.InvokeDialogueOpen(InputContext.Player, Messages);

    }

}
