using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArea : MonoBehaviour
{
    //��ϵ� Ʃ�丮�� �޽���
    public TutorialMessageSO tutorialMessage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MessageManager.Instance.tutorialMessageManager.ShowMessage(tutorialMessage);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MessageManager.Instance.tutorialMessageManager.HideMessage(tutorialMessage);
        }

    }

}
