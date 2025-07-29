using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject interacableText;
    public string npcID;

    //등록된 튜토리얼 메시지들
    public List<NPCMessageSO> NPCMessages;


    public void ShowInteractableMessage()
    {
        interacableText.SetActive(true);
    }
    public void HideInteractableMessage()
    {
        interacableText.SetActive(false);
    } 
    

}
