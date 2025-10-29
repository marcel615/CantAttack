using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParryMode : MonoBehaviour
{
    //참조할 PlayerParryHandler.cs
    [SerializeField] private PlayerParryModeHandler parryModeHandler;

    //자식 오브젝트
    [SerializeField] private Button Top;
    [SerializeField] private GameObject TopContainer;
    [SerializeField] private Button Right;
    [SerializeField] private GameObject RightContainer;
    [SerializeField] private Button Bottom;
    [SerializeField] private GameObject BottomContainer;
    [SerializeField] private Button Left;
    [SerializeField] private GameObject LeftContainer;

    //컨텍스트 enum 정보
    InputContext thisContext = InputContext.ParryMode;
    InputContext beforeContext;

    //ParryMode 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;


    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (Top == null) Top = transform.Find("Top")?.GetComponent<Button>();
        if (TopContainer == null) TopContainer = transform.Find("Top/ZeroRotationContainer").gameObject;
        if (Right == null) Right = transform.Find("Right")?.GetComponent<Button>();
        if (RightContainer == null) RightContainer = transform.Find("Right/ZeroRotationContainer").gameObject;
        if (Bottom == null) Bottom = transform.Find("Bottom")?.GetComponent<Button>();
        if (BottomContainer == null) BottomContainer = transform.Find("Bottom/ZeroRotationContainer").gameObject;
        if (Left == null) Left = transform.Find("Left")?.GetComponent<Button>();
        if (LeftContainer == null) LeftContainer = transform.Find("Left/ZeroRotationContainer").gameObject;
    }
    public void Init()
    {
        //버튼들 AddListener 달아주기
        Top.onClick.AddListener(OnClickedTop);
        Right.onClick.AddListener(OnClickedRight);
        Bottom.onClick.AddListener(OnClickedBottom);
        Left.onClick.AddListener(OnClickedLeft);
    }
    //이벤트 구독
    private void OnEnable()
    {
        //이 UI 패널 활성화되었을 때 현재 패리모드 슬롯으로 업데이트하기
        SetParryModeSlot(parryModeHandler.parryModeSlot);

        //ParryModeSlot이 업데이트되었을 때
        PlayerEvents.OnParryModeSlotUpdated += SetParryModeSlot;
    }
    private void OnDisable()
    {
        //ParryModeSlot이 업데이트되었을 때
        PlayerEvents.OnParryModeSlotUpdated -= SetParryModeSlot;
    }

    //어디선가 ParryMode 패널을 열었을 때
    public void ParryModeOpen(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, gameObject);
        InputEvents.InvokeContextUpdate(thisContext);

        //게임 시간 멈추도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(0f);
    }
    //어디선가 ParryMode 패널을 닫았을 때
    public void ParryModeClose(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        //닫기
        UIPanelController.Close(ref currentPanel, gameObject);
    }

    ///<Input>
    public void Tab(bool tab)
    {
        if (panelStack.Count > 0)
        {
            //뒤로가기
            UIPanelController.Back(panelStack, ref currentPanel);
        }
        else
        {
            //닫기
            UIPanelController.Close(ref currentPanel, gameObject);
            InputEvents.InvokeContextUpdate(InputContext.Player);
            //게임 시간 다시 흘러가도록 이벤트 발행
            SystemEvents.InvokeChangeTimeScale(1f);
        }
    }
    /// </Input>

    //ParryModeSlot이 업데이트되었을 때
    void SetParryModeSlot(ParryModeDataSO[] slots)
    {
        SetSlotPrefab(TopContainer.transform, slots[0]);
        SetSlotPrefab(RightContainer.transform, slots[1]);
        SetSlotPrefab(BottomContainer.transform, slots[2]);
        SetSlotPrefab(LeftContainer.transform, slots[3]);
    }
    //슬롯에 프리팹 채우기
    void SetSlotPrefab(Transform container, ParryModeDataSO parryModeDataSO)
    {
        //일단 Slot 안의 프리팹 제거
        if(container.childCount > 0)
        {
            Destroy(container.GetChild(0).gameObject);
        }
        //들어온 프리팹으로 채우기
        if (parryModeDataSO != null)
        {
            Instantiate(parryModeDataSO.equipIconPrefab, container.transform);
        }
    }
    void OnClickedTop()
    {
        //Debug.Log("Top Selected");
        //패리 모드 선택했다는 이벤트 발행
        PlayerEvents.InvokeParryModeSlotSelected(0);

        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(InputContext.Player);
        //게임 시간 다시 흘러가도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(1f);
    }
    void OnClickedRight()
    {
        //Debug.Log("Right Selected");
        //패리 모드 선택했다는 이벤트 발행
        PlayerEvents.InvokeParryModeSlotSelected(1);

        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(InputContext.Player);
        //게임 시간 다시 흘러가도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(1f);
    }
    void OnClickedBottom()
    {
        //Debug.Log("Bottom Selected");
        //패리 모드 선택했다는 이벤트 발행
        PlayerEvents.InvokeParryModeSlotSelected(2);

        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(InputContext.Player);
        //게임 시간 다시 흘러가도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(1f);
    }
    void OnClickedLeft()
    {
        //Debug.Log("Left Selected");
        //패리 모드 선택했다는 이벤트 발행
        PlayerEvents.InvokeParryModeSlotSelected(3);

        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(InputContext.Player);
        //게임 시간 다시 흘러가도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(1f);
    }


}
