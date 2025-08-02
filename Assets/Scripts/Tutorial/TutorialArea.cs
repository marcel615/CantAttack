using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArea : MonoBehaviour
{
    //��ϵ� Ʃ�丮�� �޽���
    public TutorialMessageSO tutorialMessage;
    bool isPlayerInside;

    //�̺�Ʈ ����
    private void OnEnable()
    {
        InputEvents.OnContextUpdate += CheckTutorialUIReOpen;
    }
    private void OnDisable()
    {
        InputEvents.OnContextUpdate -= CheckTutorialUIReOpen;

    }
    //Context�� �ٲ� �� �װ� Player����, �÷��̾ Trigger �ȿ� ��ġ�� �� �ٽ� Ʃ�丮�� �޽��� �ߵ��� �ϴ� �޼ҵ�
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
