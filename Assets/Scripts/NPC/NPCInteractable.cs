using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    //부모 오브젝트
    public NPC npc;

    //인터랙트관련 변수
    GameObject interactTarget;
    bool isInteracted;

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
    public bool IsInteractable()
    {
        return !npc.isDialogueStart;
    }
    public void Interact()
    {
        MessageManager.Instance.npcMessageManager.ShowMessage(npc.NPCMessages);
        npc.LookInteractTarget(interactTarget);
    }

}
