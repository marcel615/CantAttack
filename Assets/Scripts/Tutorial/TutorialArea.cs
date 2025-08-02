using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArea : MonoBehaviour
{
    //등록된 튜토리얼 메시지
    public TutorialMessageSO tutorialMessage;
    bool isPlayerInside;

    //이벤트 구독
    private void OnEnable()
    {
        InputEvents.OnContextUpdate += CheckTutorialUIReOpen;
    }
    private void OnDisable()
    {
        InputEvents.OnContextUpdate -= CheckTutorialUIReOpen;

    }
    //Context가 바뀔 때 그게 Player였고, 플레이어가 Trigger 안에 위치할 때 다시 튜토리얼 메시지 뜨도록 하는 메소드
    void CheckTutorialUIReOpen(InputContext context)
    {
        if((context == InputContext.Player) && isPlayerInside)
        {
            MessageManager.Instance.tutorialMessageManager.ShowMessage(tutorialMessage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
            MessageManager.Instance.tutorialMessageManager.ShowMessage(tutorialMessage);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
            MessageManager.Instance.tutorialMessageManager.HideMessage(tutorialMessage);
        }

    }

}
