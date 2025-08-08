using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    //부모 오브젝트
    public NPC npc;
    GameObject interactTarget;

    private void Awake()
    {
        npc = GetComponentInParent<NPC>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactTarget = collision.gameObject;
            npc.ShowInteractableMessage();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactTarget = null;
            npc.HideInteractableMessage();
        }

    }
    public void Interact()
    {
        MessageManager.Instance.npcMessageManager.ShowMessage(npc.NPCMessages);
        npc.LookInteractTarget(interactTarget);
    }

}
