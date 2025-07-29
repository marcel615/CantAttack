using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessageManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    public static TutorialMessageManager Instance;

    //��ϵ� Ʃ�丮�� �޽�����
    public List<TutorialMessageSO> tutorialMessages;
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
        //Tutorial ���� �޽����� �����ϵ��� ����
        TutorialEvents.InvokeTutorialOpen(Message);
    }
    public void HideMessage(TutorialMessageSO msgSO)
    {

        //Tutorial �ݵ��� ����
        TutorialEvents.InvokeTutorialClose();
    }

}
