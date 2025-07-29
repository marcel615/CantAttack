using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject interacableText;
    public string npcID;

    //��ϵ� Ʃ�丮�� �޽�����
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
