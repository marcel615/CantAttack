using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePointMenu : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private GameObject SavePointMenuSelectPanel;
    [SerializeField] private Button ShieldButton;
    [SerializeField] private Button ParryModeButton;
    [SerializeField] private Button SettingButton; 
    [SerializeField] private Button ExitButton;

    //컨텍스트 enum 정보
    InputContext thisContext = InputContext.SavePointMenu;
    InputContext beforeContext;

    //SavePointMenu 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;


    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (SavePointMenuSelectPanel == null) SavePointMenuSelectPanel = transform.Find("SavePointMenuSelectPanel")?.gameObject;
        if (ShieldButton == null) ShieldButton = transform.Find("SavePointMenuSelectPanel/ShieldButton")?.GetComponent<Button>();
        if (ParryModeButton == null) ParryModeButton = transform.Find("SavePointMenuSelectPanel/ParryModeButton")?.GetComponent<Button>();
        if (SettingButton == null) SettingButton = transform.Find("SavePointMenuSelectPanel/SettingButton")?.GetComponent<Button>();
        if (ExitButton == null) ExitButton = transform.Find("SavePointMenuSelectPanel/ExitButton")?.GetComponent<Button>();

    }
    public void Init()
    {
        //버튼들 AddListener 달아주기
        ShieldButton.onClick.AddListener(OnClickShield);
        ParryModeButton.onClick.AddListener(OnClickParryMode);
        SettingButton.onClick.AddListener(OnClickSetting);
        ExitButton.onClick.AddListener(OnClickExit);
    }

    //어디선가 SavePointMenu 패널을 열었을 때
    public void SavePointMenuOpen(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, SavePointMenuSelectPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext);

        //게임 시간 멈추도록 이벤트 발행
        //SystemEvents.InvokeChangeTimeScale(0f);
    }
    //어디선가 SavePointMenu 패널을 닫았을 때
    public void SavePointMenuClose(InputContext sourceInputContext)
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
            //SystemEvents.InvokeChangeTimeScale(1f);
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

    void OnClickShield()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        //InputEvents.InvokeContextUpdate(InputContext.Player);
        //게임 시간 다시 흘러가도록 이벤트 발행
        //SystemEvents.InvokeChangeTimeScale(1f);
    }
    void OnClickParryMode()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        //InputEvents.Setting.InvokeSettingOpen(thisContext);
    }
    void OnClickSetting()
    {
        //게임 시간 다시 흘러가도록 이벤트 발행
        //SystemEvents.InvokeChangeTimeScale(1f);
        //씬 전환 시작
        //SceneTransitionEvents.InvokeSystemMenuToMainMenu("MainMenu");
    }
    void OnClickExit()
    {
        //게임 시간 다시 흘러가도록 이벤트 발행
        //SystemEvents.InvokeChangeTimeScale(1f);
        //씬 전환 시작
        //SceneTransitionEvents.InvokeSystemMenuToMainMenu("MainMenu");
    }

}
