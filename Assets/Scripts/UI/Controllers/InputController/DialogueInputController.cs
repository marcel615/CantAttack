using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInputController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private Dialogue dialogue;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (dialogue == null) dialogue = transform.Find("UICanvas/DialoguePanel")?.GetComponent<Dialogue>();

    }

    //이벤트 구독
    private void OnEnable()
    {
        //DialogueOpen 이벤트 구독
        InputEvents.Dialogue.OnDialogueOpen += DialogueOpen;
        //Cancel 이벤트 구독
        InputEvents.Dialogue.OnCancel += ESC;
        //Submit 이벤트 구독
        InputEvents.Dialogue.OnSubmit += Enter;
        //Interact 이벤트 구독
        InputEvents.Dialogue.OnInteract += E;
    }
    private void OnDisable()
    {
        //DialogueOpen 이벤트 구독
        InputEvents.Dialogue.OnDialogueOpen -= DialogueOpen;
        //이동 이벤트 구독
        InputEvents.Dialogue.OnCancel -= ESC;
        //Submit 이벤트 구독
        InputEvents.Dialogue.OnSubmit -= Enter;
        //Interact 이벤트 구독
        InputEvents.Dialogue.OnInteract -= E;
    }

    //DialogueOpen 이벤트 구독
    void DialogueOpen(InputContext sourceInputContext, List<string> messages)
    {
        dialogue.DialogueOpen(sourceInputContext, messages);
    }

    //Cancel 이벤트 구독
    void ESC(bool esc)
    {
        if (esc)
            dialogue.ESC(esc);
    }
    //Submit 이벤트 구독
    void Enter(bool enter)
    {
        if (enter)
            dialogue.Enter(enter);
    }
    //Interact 이벤트 구독
    void E(bool e)
    {
        if (e)
            dialogue.E(e);
    }

}
