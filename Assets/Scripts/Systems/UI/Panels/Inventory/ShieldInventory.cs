using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldInventory : MonoBehaviour
{
    //부모 오브젝트
    [SerializeField] private GameObject InventoryPanel;

    //자식 오브젝트
    [SerializeField] private GameObject Shield_EquipPanel;
    [SerializeField] private List<Button> EquipSlotButtons;
    [SerializeField] private Button Shield_Slot1Button;
    [SerializeField] private Button Shield_Slot2Button;
    [SerializeField] private Button Shield_Slot3Button;
    [SerializeField] private Button Shield_Slot4Button;
    [SerializeField] private GameObject Shield_InventoryPanel;
    [SerializeField] private List<Button> InventorySlotButtons;

    //컨텍스트 enum 정보
    InputContext thisContext = InputContext.ShieldInventory;
    InputContext beforeContext;

    //ShieldInventory 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //장착하는 로직
    bool isEquiping;
    GameObject EquipIcon;
    GameObject EquipSlot;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (Shield_EquipPanel == null) Shield_EquipPanel = transform.Find("Shield_EquipPanel")?.gameObject;
        if (Shield_Slot1Button == null) Shield_Slot1Button = transform.Find("Shield_EquipPanel/Shield_Slot1Button")?.GetComponent<Button>();
        if (Shield_Slot2Button == null) Shield_Slot2Button = transform.Find("Shield_EquipPanel/Shield_Slot2Button")?.GetComponent<Button>();
        if (Shield_Slot3Button == null) Shield_Slot3Button = transform.Find("Shield_EquipPanel/Shield_Slot3Button")?.GetComponent<Button>();
        if (Shield_Slot4Button == null) Shield_Slot4Button = transform.Find("Shield_EquipPanel/Shield_Slot4Button")?.GetComponent<Button>();

        if (Shield_InventoryPanel == null) Shield_InventoryPanel = transform.Find("Shield_InventoryPanel")?.gameObject;

    }
    public void Init()
    {
        //버튼들 AddListener 달아주기
        /*
        Shield_Slot1Button.onClick.AddListener(OnClickShield_Slot1);
        Shield_Slot2Button.onClick.AddListener(OnClickShield_Slot2);
        Shield_Slot3Button.onClick.AddListener(OnClickShield_Slot3);
        Shield_Slot4Button.onClick.AddListener(OnClickShield_Slot4);
        */

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

    //어디선가 ShieldInventory 패널을 열었을 때
    public void ShieldInventoryOpen(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, InventoryPanel);
        InputEvents.InvokeContextUpdate(thisContext);

        //게임 시간 멈추도록 이벤트 발행
        //SystemEvents.InvokeChangeTimeScale(0f);
    }
    //어디선가 ShieldInventory 패널을 닫았을 때
    public void ShieldInventoryClose(InputContext sourceInputContext)
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

    void OnClickShield_Slot1()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        //InputEvents.InvokeContextUpdate(InputContext.Player);
        //게임 시간 다시 흘러가도록 이벤트 발행
        //SystemEvents.InvokeChangeTimeScale(1f);
    }
    void OnClickShield_Slot2()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        //InputEvents.Setting.InvokeSettingOpen(thisContext);
    }
    void OnClickShield_Slot3()
    {
        //게임 시간 다시 흘러가도록 이벤트 발행
        //SystemEvents.InvokeChangeTimeScale(1f);
        //씬 전환 시작
        //SceneTransitionEvents.InvokeSystemMenuToMainMenu("MainMenu");
    }
    void OnClickShield_Slot4()
    {
        //게임 시간 다시 흘러가도록 이벤트 발행
        //SystemEvents.InvokeChangeTimeScale(1f);
        //씬 전환 시작
        //SceneTransitionEvents.InvokeSystemMenuToMainMenu("MainMenu");
    }
    void OnClickEquipSlot(int index)
    {
        Debug.Log("Test" + index);

        EquipSlot = EquipSlotButtons[index].gameObject;

        if (!isEquiping)
        {
            isEquiping = true;

            //장착 슬롯 버튼들 비활성화
            foreach (var btn in EquipSlotButtons)
                btn.interactable = false;

            //상호작용 가능한 버튼 포커스 되도록
            InputEvents.InvokeSelectFirstSelectable(currentPanel);
        }
        else
        {
            foreach(Transform child in EquipSlotButtons[index].transform)
                Destroy(child.gameObject);

            Instantiate(EquipIcon, EquipSlot.transform);

            //인벤토리 슬롯 버튼들 활성화
            foreach (var btn in InventorySlotButtons)
                btn.interactable = true;

            EquipSlot.GetComponent<Selectable>().Select();

            EquipSlot = null;
            EquipIcon = null;

            isEquiping = false;
        }
    }
    void OnClickInventorySlot(int index)
    {
        Debug.Log("Test" + index);

        EquipIcon = InventorySlotButtons[index].GetComponentInChildren<SlotIconUI>().IconPrefab;

        if (!isEquiping)
        {
            isEquiping = true;

            //인벤토리 슬롯 버튼들 비활성화
            foreach (var btn in InventorySlotButtons)
                btn.interactable = false;

            //상호작용 가능한 버튼 포커스 되도록
            InputEvents.InvokeSelectFirstSelectable(currentPanel);
        }
        else
        {
            foreach (Transform child in EquipSlot.transform)
                Destroy(child.gameObject);

            Instantiate(EquipIcon, EquipSlot.transform);

            //장착 슬롯 버튼들 활성화
            foreach (var btn in EquipSlotButtons)
                btn.interactable = true;

            EquipSlot.GetComponent<Selectable>().Select();

            EquipSlot = null;
            EquipIcon = null;

            isEquiping = false;
        }

    }

}
