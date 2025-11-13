using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ParryMode : MonoBehaviour
{
    //참조할 PlayerParryHandler.cs
    [SerializeField] private PlayerParryModeHandler playerParryModeHandler;

    //자식 오브젝트
    [SerializeField] private List<Button> SlotButtons;
    [SerializeField] private List<GameObject> SlotContainers;

    //컨텍스트 enum 정보
    InputContext thisContext = InputContext.ParryMode;
    InputContext beforeContext;

    //ParryMode 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //ParryModeSlot 하이라이트 관련
    GameObject prevSelectedIcon;
    GameObject selectedIcon;


    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
    }
    public void Init()
    {
        //각 슬롯 버튼에 애드리스너 달아주기
        for (int i = 0; i < SlotButtons.Count; i++)
        {
            int index = i;
            SlotButtons[i].onClick.AddListener(() => OnClickSlot(index));
        }

    }
    private void Update()
    {
        selectedIcon = EventSystem.current.currentSelectedGameObject;
        if(selectedIcon != prevSelectedIcon)
        {
            HighlightParryModeSlot();
            prevSelectedIcon = selectedIcon;
        }
    }

    //이벤트 구독
    private void OnEnable()
    {
        //이 UI 패널 활성화되었을 때 현재 패리모드 슬롯으로 업데이트하기
        SetParryModeSlot();
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

    //패리모드 UI 켜질때 현재 패리모드 슬롯상태 가져와서 띄워주기
    void SetParryModeSlot()
    {
        ParryModeDataSO[] parryModeDataSOs = playerParryModeHandler.GetParryModeSlots();

        for (int i = 0; i < parryModeDataSOs.Length; i++)
        {
            foreach (Transform child in SlotContainers[i].transform)
                Destroy(child?.gameObject);

            GameObject icon = parryModeDataSOs[i]?.equipIconPrefab;
            if (icon != null)
                Instantiate(icon, SlotContainers[i].transform);
        }
    }

    //ParryModeSlot 선택 변경 될 때
    void HighlightParryModeSlot()
    {
        //이전에 선택된 아이콘 하이라이트 초기화
        if (prevSelectedIcon != null)
            prevSelectedIcon.transform.localScale = Vector3.one;

        //선택된 아이콘 하이라이트
        if(selectedIcon != null)        
            selectedIcon.transform.localScale = Vector3.one * 1.1f;
        
    }

    void OnClickSlot(int index)
    {
        //패리 모드 선택했다는 이벤트 발행
        PlayerEvents.InvokeParryModeSlotSelected(index);

        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(InputContext.Player);
        //게임 시간 다시 흘러가도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(1f);
    }

}
