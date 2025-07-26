using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotInputController : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private SaveSlot saveSlot;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (saveSlot == null) saveSlot = transform.Find("UICanvas/SaveSlotPanel")?.GetComponent<SaveSlot>();

    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //SaveSlotOpen �̺�Ʈ ����
        InputEvents.SaveSlot.OnSaveSlotOpen += SaveSlotOpen;
        //Cancel �̺�Ʈ ����
        InputEvents.SaveSlot.OnCancel += ESC;
        //Submit �̺�Ʈ ����
        InputEvents.SaveSlot.OnSubmit += Enter;
        //Interact �̺�Ʈ ����
        InputEvents.SaveSlot.OnInteract += E;
    }
    private void OnDisable()
    {
        //SaveSlotOpen �̺�Ʈ ����
        InputEvents.SaveSlot.OnSaveSlotOpen -= SaveSlotOpen;
        //�̵� �̺�Ʈ ����
        InputEvents.SaveSlot.OnCancel -= ESC;
        //Submit �̺�Ʈ ����
        InputEvents.SaveSlot.OnSubmit -= Enter;
        //Interact �̺�Ʈ ����
        InputEvents.SaveSlot.OnInteract -= E;
    }

    //SettingOpen �̺�Ʈ ����
    void SaveSlotOpen(InputContext sourceInputContext)
    {
        saveSlot.SaveSlotOpen(sourceInputContext);
    }

    //Cancel �̺�Ʈ ����
    void ESC(bool esc)
    {
        if (esc)
            saveSlot.ESC(esc);
    }
    //Submit �̺�Ʈ ����
    void Enter(bool enter)
    {
        if (enter)
            saveSlot.Enter(enter);
    }
    //Interact �̺�Ʈ ����
    void E(bool e)
    {
        if (e)
            saveSlot.E(e);
    }

}
