using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMessageManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    public static NPCMessageManager Instance;

    //��ϵ� NPC �޽�����
    public List<NPCMessageSO> NPCMessages;
    //�޽������ ��ųʸ� �����
    private Dictionary<string, string> messageDict;



    private void Awake()
    {
        // ���� �ν��Ͻ��� ������ �� && ���� ���ο� �ν��Ͻ��� �����Ƿ��� �� ��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //�ߺ����� �ʵ��� ���� ���Ӱ� �����Ǵ� ���� �ı���Ų��
            return;
        }
        // �ν��Ͻ� ó�� �Ҵ�
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
        //Dialogue ���� �޽�������Ʈ�� �����ϵ��� ����
        InputEvents.Dialogue.InvokeDialogueOpen(InputContext.Player, Messages);

    }

}
