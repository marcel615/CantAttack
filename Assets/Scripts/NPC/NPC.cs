using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject interactableText;
    public string npcID;

    //등록된 튜토리얼 메시지들
    public List<NPCMessageSO> NPCMessages;

    //플레이어와 대화 관련 변수
    bool isInteracting;
    Vector3 originalLocalScale;

    private void Awake()
    {
        originalLocalScale = transform.localScale;
    }

    public void ShowInteractableMessage()
    {
        interactableText.SetActive(true);
    }
    public void HideInteractableMessage()
    {
        interactableText.SetActive(false);
    }
    public void LookInteractTarget(GameObject interatTarget)
    {
        isInteracting = true;
        interactableText.SetActive(false);

        if (interatTarget.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    //이벤트 구독
    private void OnEnable()
    {
        InputEvents.Dialogue.OnDialogueClose += OnDialogueClose;
    }
    private void OnDisable()
    {
        InputEvents.Dialogue.OnDialogueClose -= OnDialogueClose;
    }
    void OnDialogueClose()
    {
        isInteracting = false;
        interactableText.SetActive(true);

        transform.localScale = originalLocalScale;
    }
    

}
