using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointInteractable : MonoBehaviour, IInteractable
{
    //�θ� ������Ʈ
    public SavePoint savePoint;

    //���ͷ�Ʈ���� ����
    GameObject interactTarget;
    bool isInteracted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactTarget = collision.gameObject;
            savePoint.ShowInteractableMessage();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInteracted = false;

            interactTarget = null;
            savePoint.HideInteractableMessage();
        }
    }
    public void Interact()
    {
        //���ͷ�Ʈ �� �� �� ���¸� ����
        if (isInteracted) return;

        isInteracted = true;
        savePoint.DoSave(interactTarget);
    }

}
