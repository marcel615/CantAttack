using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    //등록된 튜토리얼 메시지들
    public List<TutorialMessageSO> tutorialMessages;

    //한 번 실행되었는지 플래그
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
