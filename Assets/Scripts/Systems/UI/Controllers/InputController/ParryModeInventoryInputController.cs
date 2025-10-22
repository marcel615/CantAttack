using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryModeInventoryInputController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private ParryModeInventory parryModeInventory;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (parryModeInventory == null) parryModeInventory = transform.Find("UICanvas/InventoryPanel/ParryModeInventoryPanel")?.GetComponent<ParryModeInventory>();

    }

    //이벤트 구독
    private void OnEnable()
    {
        //ParryModeInventoryOpen 이벤트 구독
        InputEvents.ParryModeInventory.OnParryModeInventoryOpen += ParryModeInventoryOpen;
        //ParryModeInventoryClose 이벤트 구독
        InputEvents.ParryModeInventory.OnParryModeInventoryClose += ParryModeInventoryClose;
        //Cancel 이벤트 구독
        InputEvents.ParryModeInventory.OnCancel += ESC;
        //Submit 이벤트 구독
        InputEvents.ParryModeInventory.OnSubmit += Enter;
        //Interact 이벤트 구독
        InputEvents.ParryModeInventory.OnInteract += E;
    }
    private void OnDisable()
    {
        //ParryModeInventoryOpen 이벤트 구독
        InputEvents.ParryModeInventory.OnParryModeInventoryOpen -= ParryModeInventoryOpen;
        //ParryModeInventoryClose 이벤트 구독
        InputEvents.ParryModeInventory.OnParryModeInventoryClose -= ParryModeInventoryClose;
        //Cancel 이벤트 구독
        InputEvents.ParryModeInventory.OnCancel -= ESC;
        //Submit 이벤트 구독
        InputEvents.ParryModeInventory.OnSubmit -= Enter;
        //Interact 이벤트 구독
        InputEvents.ParryModeInventory.OnInteract -= E;
    }

    //ParryModeInventoryOpen 이벤트 구독
    void ParryModeInventoryOpen(InputContext sourceInputContext)
    {
        parryModeInventory.ParryModeInventoryOpen(sourceInputContext);
    }
    //ParryModeInventoryClose 이벤트 구독
    void ParryModeInventoryClose(InputContext sourceInputContext)
    {
        parryModeInventory.ParryModeInventoryClose(sourceInputContext);
    }

    //Cancel 이벤트 구독
    void ESC(bool esc)
    {
        if (esc)
            parryModeInventory.ESC(esc);
    }
    //Submit 이벤트 구독
    void Enter(bool enter)
    {
        if (enter)
            parryModeInventory.Enter(enter);
    }
    //Interact 이벤트 구독
    void E(bool e)
    {
        if (e)
            parryModeInventory.E(e);
    }

}
