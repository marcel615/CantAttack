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
    public InputContext thisContext = InputContext.SystemMenu;
    public InputContext beforeContext;

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

    //이벤트 구독
    private void OnEnable()
    {
        //OnClickSaveAndExit 이후 세이브 완료되면 발행되는 이벤트 구독
        SystemEvents.OnSaveEnd += SaveEnd;
    }
    private void OnDisable()
    {
        //OnClickSaveAndExit 이후 세이브 완료되면 발행되는 이벤트 구독
        SystemEvents.OnSaveEnd -= SaveEnd;
    }

    //어디선가 SystemMenu 패널을 열었을 때
    public void SystemMenuOpen(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, SystemMenuSelectPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext, true);
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
            InputEvents.InvokeContextUpdate(thisContext, false);
            if (beforeContext == InputContext.Player)
            {
                InputEvents.InvokeContextUpdate(InputContext.Player, true);
            }
            if (beforeContext == InputContext.Setting)
            {
                InputEvents.InvokeContextUpdate(InputContext.Player, true);
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

    void OnClickContinue()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext, false);
        InputEvents.InvokeContextUpdate(InputContext.Player, true);
    }
    void OnClickSetting()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext, false);
        InputEvents.InvokeContextUpdate(InputContext.Setting, true);
        InputEvents.Setting.InvokeSettingOpen(thisContext);
    }
    void OnClickSaveAndExit()
    {
        SystemEvents.InvokeSaveRequest();
    }
    //OnClickSaveAndExit 이후 세이브 완료되면 발행되는 이벤트 구독
    void SaveEnd()
    {
        //현재 UI 닫고 메인메뉴로 나가기
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext, false);
        InputEvents.InvokeContextUpdate(InputContext.MainMenu, true);
        InputEvents.MainMenu.InvokeMainMenuOpen(thisContext);
        // 메인메뉴 씬 로드
        SceneManager.LoadScene("MainMenu");
    }


}
