using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //���ؽ�Ʈ enum ����
    public InputContext thisContext = InputContext.Player;

    //IInteractable ������Ʈ
    IInteractable interactableTarget;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactableTarget = interactable;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            if(interactable == interactableTarget)
            {
                interactableTarget = null;
            }
        }
    }
    ///<Input>
    public void E(bool e)
    {
        if(interactableTarget != null)
        {
            interactableTarget.Interact();
        }
    }
    /// </Input>
}
