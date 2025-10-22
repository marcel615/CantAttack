using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParryModeInventory : MonoBehaviour
{
    //부모 오브젝트
    [SerializeField] private GameObject InventoryPanel;

    //자식 오브젝트
    [SerializeField] private GameObject ParryMode_EquipPanel;
    [SerializeField] private Button ParryMode_SlotTopButton;
    [SerializeField] private GameObject TopContainer;
    [SerializeField] private Button ParryMode_SlotRightButton;
    [SerializeField] private GameObject RightContainer;
    [SerializeField] private Button ParryMode_SlotBottomButton;
    [SerializeField] private GameObject BottomContainer;
    [SerializeField] private Button ParryMode_SlotLeftButton;
    [SerializeField] private GameObject LeftContainer;

    [SerializeField] private GameObject ParryMode_InventoryPanel;

    //컨텍스트 enum 정보
    InputContext thisContext = InputContext.ParryModeInventory;
    InputContext beforeContext;

    //ParryModeInventory 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;


    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (ParryMode_EquipPanel == null) ParryMode_EquipPanel = transform.Find("ParryMode_EquipPanel")?.gameObject;
        if (ParryMode_SlotTopButton == null) ParryMode_SlotTopButton = transform.Find("ParryMode_EquipPanel/Top")?.GetComponent<Button>();
        if (ParryMode_SlotRightButton == null) ParryMode_SlotRightButton = transform.Find("ParryMode_EquipPanel/Right")?.GetComponent<Button>();
        if (ParryMode_SlotBottomButton == null) ParryMode_SlotBottomButton = transform.Find("ParryMode_EquipPanel/Bottom")?.GetComponent<Button>();
        if (ParryMode_SlotLeftButton == null) ParryMode_SlotLeftButton = transform.Find("ParryMode_EquipPanel/Left")?.GetComponent<Button>();

        if (ParryMode_InventoryPanel == null) ParryMode_InventoryPanel = transform.Find("ParryMode_InventoryPanel")?.gameObject;

    }
    public void Init()
    {
        //버튼들 AddListener 달아주기
        ParryMode_SlotTopButton.onClick.AddListener(OnClickParryMode_SlotTop);
        ParryMode_SlotRightButton.onClick.AddListener(OnClickParryMode_SlotRight);
        ParryMode_SlotBottomButton.onClick.AddListener(OnClickParryMode_SlotBottom);
        ParryMode_SlotLeftButton.onClick.AddListener(OnClickParryMode_SlotLeft);
    }

    //어디선가 ParryModeInventory 패널을 열었을 때
    public void ParryModeInventoryOpen(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, InventoryPanel);
        InputEvents.InvokeContextUpdate(thisContext);

        //게임 시간 멈추도록 이벤트 발행
        //SystemEvents.InvokeChangeTimeScale(0f);
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

    void OnClickParryMode_SlotTop()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        //InputEvents.InvokeContextUpdate(InputContext.Player);
        //게임 시간 다시 흘러가도록 이벤트 발행
        //SystemEvents.InvokeChangeTimeScale(1f);
    }
    void OnClickParryMode_SlotRight()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        //InputEvents.Setting.InvokeSettingOpen(thisContext);
    }
    void OnClickParryMode_SlotBottom()
    {
        //게임 시간 다시 흘러가도록 이벤트 발행
        //SystemEvents.InvokeChangeTimeScale(1f);
        //씬 전환 시작
        //SceneTransitionEvents.InvokeSystemMenuToMainMenu("MainMenu");
    }
    void OnClickParryMode_SlotLeft()
    {
        //게임 시간 다시 흘러가도록 이벤트 발행
        //SystemEvents.InvokeChangeTimeScale(1f);
        //씬 전환 시작
        //SceneTransitionEvents.InvokeSystemMenuToMainMenu("MainMenu");
    }

}
