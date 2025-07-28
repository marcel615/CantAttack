using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    //��ϵ� Ʃ�丮�� �޽�����
    public List<TutorialMessageSO> tutorialMessages;

    //�� �� ����Ǿ����� �÷���
    bool isTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isTriggered) return;
        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
            MessageManager.Instance.tutorialMessageManager.ShowMessage(tutorialMessages);
        }
        
    }

}
