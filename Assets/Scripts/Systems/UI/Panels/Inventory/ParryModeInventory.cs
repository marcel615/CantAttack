using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParryModeInventory : MonoBehaviour
{
    //다른 오브젝트
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private PlayerParryModeHandler playerParryModeHandler;

    //부모 오브젝트
    [SerializeField] private GameObject InventoryPanel;

    //자식 오브젝트
    [SerializeField] private GameObject ParryMode_EquipPanel;
    [SerializeField] private List<Button> EquipSlotButtons;
    [SerializeField] private List<GameObject> EquipSlotContainers;

    [SerializeField] private GameObject ParryMode_InventoryPanel;
    [SerializeField] private List<Button> InventorySlotButtons;

    //컨텍스트 enum 정보
    InputContext thisContext = InputContext.ParryModeInventory;
    InputContext beforeContext;

    //ParryModeInventory 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //장착하는 로직
    bool isEquiping;
    ParryModeDataSO equipParryModeDataSO;
    int equipIndex;

    //EmptyParryModeSO
    [SerializeField] private ParryModeDataSO EmptyParryModeSO;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (ParryMode_EquipPanel == null) ParryMode_EquipPanel = transform.Find("ParryMode_EquipPanel")?.gameObject;
        if (ParryMode_InventoryPanel == null) ParryMode_InventoryPanel = transform.Find("ParryMode_InventoryPanel")?.gameObject;

    }
    public void Init()
    {
        //각 장착 버튼에 애드리스너 달아주기
        for (int i = 0; i < EquipSlotButtons.Count; i++)
        {
            int index = i;
            EquipSlotButtons[i].onClick.AddListener(() => OnClickEquipSlot(index));
        }
        //각 인벤토리 버튼에 애드리스너 달아주기
        for (int i = 0; i < InventorySlotButtons.Count; i++)
        {
            int index = i;
            InventorySlotButtons[i].onClick.AddListener(() => OnClickInventorySlot(index));
        }
    }
    private void OnEnable()
    {
        //인벤토리 UI 켜질때 인벤토리 슬롯들 가져와서 보여주기
        UpdateEquipSlot();
        UpdateInventory();

        //ParryModeSlot이 업데이트되었을 때
        PlayerEvents.OnParryModeSlotUpdated += UpdateEquipSlot;
    }
    private void OnDisable()
    {
        //ParryModeSlot이 업데이트되었을 때
        PlayerEvents.OnParryModeSlotUpdated -= UpdateEquipSlot;
    }

    //어디선가 ParryModeInventory 패널을 열었을 때
    public void ParryModeInventoryOpen(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, InventoryPanel);
        InputEvents.InvokeContextUpdate(thisContext);

    }
    //어디선가 ParryModeInventory 패널을 닫았을 때
    public void ParryModeInventoryClose(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        //닫기
        UIPanelController.Close(ref currentPanel, gameObject);
    }

    ///<Input>
    public void ESC(bool esc)
    {
        if (panelStack.Count > 0)
        {
            //뒤로가기
            UIPanelController.Back(panelStack, ref currentPanel);
        }
        else
        {
            //닫기
            UIPanelController.Close(ref currentPanel, InventoryPanel);
            if (beforeContext == InputContext.SavePointMenu)
            {
                InputEvents.SavePointMenu.InvokeSavePointMenuOpen(thisContext);
            }
        }
    }
    public void Enter(bool enter)
    {

    }
    public void E(bool e)
    {
        UIUtility.TriggerSelectAction();
    }
    /// </Input>

    void UpdateEquipSlot(ParryModeDataSO[] slot = null)
    {
        ParryModeDataSO[] parryModeDataSOs = playerParryModeHandler.GetParryModeSlots();

        for (int i = 0; i < parryModeDataSOs.Length; i++)
        {
            foreach (Transform child in EquipSlotContainers[i].transform)
                Destroy(child?.gameObject);

            GameObject icon = parryModeDataSOs[i]?.equipIconPrefab;
            if (icon != null)
                Instantiate(icon, EquipSlotContainers[i].transform);
        }
    }
    void UpdateInventory()
    {
        IReadOnlyList<ParryModeDataSO> parryModeDataSOs = inventoryManager.GetParryModeInventory();

        for (int i = 0; i < InventorySlotButtons.Count; i++)
        {
            foreach (Transform child in InventorySlotButtons[i].transform)
                Destroy(child?.gameObject);

            if (i < parryModeDataSOs.Count)
            {
                GameObject icon = parryModeDataSOs[i].inventoryIconPrefab;
                Instantiate(icon, InventorySlotButtons[i].transform);
            }
            else
            {
                GameObject icon = EmptyParryModeSO.inventoryIconPrefab;
                Instantiate(icon, InventorySlotButtons[i].transform);
            }
        }
    }
    void OnClickEquipSlot(int index)
    {
        //장착 슬롯 인덱스 저장
        equipIndex = index;

        //슬롯 교체 이전
        if (!isEquiping)
        {
            isEquiping = true;

            //장착 슬롯 버튼들 비활성화
            foreach (var btn in EquipSlotButtons)
                btn.interactable = false;

            //상호작용 가능한 버튼 포커스 되도록
            InputEvents.InvokeSelectFirstSelectable(ParryMode_InventoryPanel);
        }
        else //슬롯 교체
        {
            //슬롯 교체 해달라고 이벤트 발행
            PlayerEvents.InvokeRequestEquipParryMode(equipParryModeDataSO, equipIndex);

            //인벤토리 슬롯 버튼들 활성화
            foreach (var btn in InventorySlotButtons)
                btn.interactable = true;

            //지금 막 교체된 슬롯에 포커스되도록
            EquipSlotButtons[equipIndex].GetComponent<Selectable>().Select();

            //변수 초기화
            equipIndex = 0;
            equipParryModeDataSO = null;
            isEquiping = false;
        }
    }
    void OnClickInventorySlot(int index)
    {
        //장착 데이터 저장
        if (index < inventoryManager.GetParryModeInventory().Count)
            equipParryModeDataSO = inventoryManager.GetParryModeInventory()[index];
        else
            equipParryModeDataSO = EmptyParryModeSO;

        //슬롯 교체 이전
        if (!isEquiping)
        {
            isEquiping = true;

            //인벤토리 슬롯 버튼들 비활성화
            foreach (var btn in InventorySlotButtons)
                btn.interactable = false;

            //상호작용 가능한 버튼 포커스 되도록
            InputEvents.InvokeSelectFirstSelectable(ParryMode_EquipPanel);
        }
        else //슬롯 교체
        {
            //슬롯 교체 해달라고 이벤트 발행
            PlayerEvents.InvokeRequestEquipParryMode(equipParryModeDataSO, equipIndex);

            //장착 슬롯 버튼들 활성화
            foreach (var btn in EquipSlotButtons)
                btn.interactable = true;

            //지금 막 교체된 슬롯에 포커스되도록
            EquipSlotButtons[equipIndex].GetComponent<Selectable>().Select();

            //변수 초기화
            equipIndex = 0;
            equipParryModeDataSO = null;
            isEquiping = false;
        }
    }

}
