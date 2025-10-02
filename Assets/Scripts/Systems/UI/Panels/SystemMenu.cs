using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SystemMenu : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private GameObject SystemMenuSelectPanel;
    [SerializeField] private Button ContinueButton;
    [SerializeField] private Button SettingButton;
    [SerializeField] private Button SaveAndExitButton;

    //컨텍스트 enum 정보
    InputContext thisContext = InputContext.SystemMenu;
    InputContext beforeContext;

    //SystemMenu 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;


    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (SystemMenuSelectPanel == null) SystemMenuSelectPanel = transform.Find("SystemMenuSelectPanel")?.gameObject;
        if (ContinueButton == null) ContinueButton = transform.Find("SystemMenuSelectPanel/ContinueButton")?.GetComponent<Button>();
        if (SettingButton == null) SettingButton = transform.Find("SystemMenuSelectPanel/SettingButton")?.GetComponent<Button>();
        if (SaveAndExitButton == null) SaveAndExitButton = transform.Find("SystemMenuSelectPanel/SaveAndExitButton")?.GetComponent<Button>();

    }
    public void Init()
    {
        //버튼들 AddListener 달아주기
        ContinueButton.onClick.AddListener(OnClickContinue);
        SettingButton.onClick.AddListener(OnClickSetting);
        SaveAndExitButton.onClick.AddListener(OnClickSaveAndExit);
    }

    //어디선가 SystemMenu 패널을 열었을 때
    public void SystemMenuOpen(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, SystemMenuSelectPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext);

        //게임 시간 멈추도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(0f);
    }
    //어디선가 SystemMenu 패널을 닫았을 때
    public void SystemMenuClose(InputContext sourceInputContext)
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
            UIPanelController.Close(ref currentPanel, gameObject);
            InputEvents.InvokeContextUpdate(InputContext.Player);
            //게임 시간 다시 흘러가도록 이벤트 발행
            SystemEvents.InvokeChangeTimeScale(1f);
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

    void OnClickContinue()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(InputContext.Player);
        //게임 시간 다시 흘러가도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(1f);
    }
    void OnClickSetting()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.Setting.InvokeSettingOpen(thisContext);
    }
    void OnClickSaveAndExit()
    {
        //게임 시간 다시 흘러가도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(1f);
        //씬 전환 시작
        SceneTransitionEvents.InvokeSystemMenuToMainMenu("MainMenu");
    }


}
