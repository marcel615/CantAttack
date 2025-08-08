using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointInteractable : MonoBehaviour, IInteractable
{
    //부모 오브젝트
    public SavePoint savePoint;

    //인터랙트관련 변수
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
        //인터랙트 한 번 한 상태면 종료
        if (isInteracted) return;

        isInteracted = true;
        savePoint.DoSave(interactTarget);
    }

}
