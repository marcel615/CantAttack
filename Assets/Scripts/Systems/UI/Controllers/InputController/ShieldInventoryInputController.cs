using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldInventoryInputController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private ShieldInventory shieldInventory;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (shieldInventory == null) shieldInventory = transform.Find("UICanvas/InventoryPanel/ShieldInventoryPanel")?.GetComponent<ShieldInventory>();

    }

    //이벤트 구독
    private void OnEnable()
    {
        //ShieldInventoryOpen 이벤트 구독
        InputEvents.ShieldInventory.OnShieldInventoryOpen += ShieldInventoryOpen;
        //ShieldInventoryClose 이벤트 구독
        InputEvents.ShieldInventory.OnShieldInventoryClose += ShieldInventoryClose;
        //Cancel 이벤트 구독
        InputEvents.ShieldInventory.OnCancel += ESC;
        //Submit 이벤트 구독
        InputEvents.ShieldInventory.OnSubmit += Enter;
        //Interact 이벤트 구독
        InputEvents.ShieldInventory.OnInteract += E;
    }
    private void OnDisable()
    {
        //ShieldInventoryOpen 이벤트 구독
        InputEvents.ShieldInventory.OnShieldInventoryOpen -= ShieldInventoryOpen;
        //ShieldInventoryClose 이벤트 구독
        InputEvents.ShieldInventory.OnShieldInventoryClose -= ShieldInventoryClose;
        //Cancel 이벤트 구독
        InputEvents.ShieldInventory.OnCancel -= ESC;
        //Submit 이벤트 구독
        InputEvents.ShieldInventory.OnSubmit -= Enter;
        //Interact 이벤트 구독
        InputEvents.ShieldInventory.OnInteract -= E;
    }

    //ShieldInventoryOpen 이벤트 구독
    void ShieldInventoryOpen(InputContext sourceInputContext)
    {
        shieldInventory.ShieldInventoryOpen(sourceInputContext);
    }
    //ShieldInventoryClose 이벤트 구독
    void ShieldInventoryClose(InputContext sourceInputContext)
    {
        shieldInventory.ShieldInventoryClose(sourceInputContext);
    }

    //Cancel 이벤트 구독
    void ESC(bool esc)
    {
        if (esc)
            shieldInventory.ESC(esc);
    }
    //Submit 이벤트 구독
    void Enter(bool enter)
    {
        if (enter)
            shieldInventory.Enter(enter);
    }
    //Interact 이벤트 구독
    void E(bool e)
    {
        if (e)
            shieldInventory.E(e);
    }
}
