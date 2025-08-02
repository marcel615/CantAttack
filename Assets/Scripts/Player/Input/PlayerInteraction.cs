using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //컨텍스트 enum 정보
    public InputContext thisContext = InputContext.Player;

    //IInteractable 오브젝트
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
