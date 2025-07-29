using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMessageManager : MonoBehaviour
{
    //��ϵ� NPC �޽�����
    public List<NPCMessageSO> NPCMessages;
    //�޽������ ��ųʸ� �����
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
        //Dialogue ���� �޽�������Ʈ�� �����ϵ��� ����
        InputEvents.Dialogue.InvokeDialogueOpen(InputContext.Player, Messages);

    }

}
