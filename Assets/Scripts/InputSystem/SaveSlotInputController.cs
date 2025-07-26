using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotInputController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private SaveSlot saveSlot;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (saveSlot == null) saveSlot = transform.Find("UICanvas/SaveSlotPanel")?.GetComponent<SaveSlot>();

    }

    //이벤트 구독
    private void OnEnable()
    {
        //SaveSlotOpen 이벤트 구독
        InputEvents.SaveSlot.OnSaveSlotOpen += SaveSlotOpen;
        //Cancel 이벤트 구독
        InputEvents.SaveSlot.OnCancel += ESC;
        //Submit 이벤트 구독
        InputEvents.SaveSlot.OnSubmit += Enter;
        //Interact 이벤트 구독
        InputEvents.SaveSlot.OnInteract += E;
    }
    private void OnDisable()
    {
        //SaveSlotOpen 이벤트 구독
        InputEvents.SaveSlot.OnSaveSlotOpen -= SaveSlotOpen;
        //이동 이벤트 구독
        InputEvents.SaveSlot.OnCancel -= ESC;
        //Submit 이벤트 구독
        InputEvents.SaveSlot.OnSubmit -= Enter;
        //Interact 이벤트 구독
        InputEvents.SaveSlot.OnInteract -= E;
    }

    //SettingOpen 이벤트 구독
    void SaveSlotOpen(InputContext sourceInputContext)
    {
        saveSlot.SaveSlotOpen(sourceInputContext);
    }

    //Cancel 이벤트 구독
    void ESC(bool esc)
    {
        if (esc)
            saveSlot.ESC(esc);
    }
    //Submit 이벤트 구독
    void Enter(bool enter)
    {
        if (enter)
            saveSlot.Enter(enter);
    }
    //Interact 이벤트 구독
    void E(bool e)
    {
        if (e)
            saveSlot.E(e);
    }

}
