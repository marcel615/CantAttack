using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMessageManager : MonoBehaviour
{
    //등록된 NPC 메시지들
    public List<NPCMessageSO> NPCMessages;
    //메시지들로 딕셔너리 만들기
    private Dictionary<string, string> messageDict;



    private void Awake()
    {
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
