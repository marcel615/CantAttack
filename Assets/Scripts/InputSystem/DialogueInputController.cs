using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInputController : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private Dialogue dialogue;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (dialogue == null) dialogue = transform.Find("UICanvas/DialoguePanel")?.GetComponent<Dialogue>();

    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //DialogueOpen �̺�Ʈ ����
        InputEvents.Dialogue.OnDialogueOpen += DialogueOpen;
        //Cancel �̺�Ʈ ����
        InputEvents.Dialogue.OnCancel += ESC;
        //Submit �̺�Ʈ ����
        InputEvents.Dialogue.OnSubmit += Enter;
        //Interact �̺�Ʈ ����
        InputEvents.Dialogue.OnInteract += E;
    }
    private void OnDisable()
    {
        //DialogueOpen �̺�Ʈ ����
        InputEvents.Dialogue.OnDialogueOpen -= DialogueOpen;
        //�̵� �̺�Ʈ ����
        InputEvents.Dialogue.OnCancel -= ESC;
        //Submit �̺�Ʈ ����
        InputEvents.Dialogue.OnSubmit -= Enter;
        //Interact �̺�Ʈ ����
        InputEvents.Dialogue.OnInteract -= E;
    }

    //DialogueOpen �̺�Ʈ ����
    void DialogueOpen(InputContext sourceInputContext, List<string> messages)
    {
        dialogue.DialogueOpen(sourceInputContext, messages);
    }

    //Cancel �̺�Ʈ ����
    void ESC(bool esc)
    {
        if (esc)
            dialogue.ESC(esc);
    }
    //Submit �̺�Ʈ ����
    void Enter(bool enter)
    {
        if (enter)
            dialogue.Enter(enter);
    }
    //Interact �̺�Ʈ ����
    void E(bool e)
    {
        if (e)
            dialogue.E(e);
    }

}
