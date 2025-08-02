using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private GameObject SettingSelectPanel;
    [SerializeField] private GameObject GamePlayPanel;
    [SerializeField] private GameObject GraphicsPanel;
    [SerializeField] private GameObject AudioPanel;
    [SerializeField] private GameObject ControlsPanel;
    [SerializeField] private GameObject UIAndAccessibilityPanel;

    [SerializeField] private Button GamePlayButton;
    [SerializeField] private Button GraphicsButton;
    [SerializeField] private Button AudioButton;
    [SerializeField] private Button ControlsButton;
    [SerializeField] private Button UIAndAccessibilityButton;

    //컨텍스트 enum 정보
    InputContext thisContext = InputContext.Setting;
    InputContext beforeContext;

    //Setting 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;
    Color InGameBackGroundColor;
    Color MainMenuBackGroundColor = new Color32(110, 110, 110, 255); //메인메뉴 배경 색


    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (SettingSelectPanel == null) SettingSelectPanel = transform.Find("SettingSelectPanel")?.gameObject;
        if (GamePlayPanel == null) GamePlayPanel = transform.Find("GamePlayPanel")?.gameObject;
        if (GraphicsPanel == null) GraphicsPanel = transform.Find("GraphicsPanel")?.gameObject;
        if (AudioPanel == null) AudioPanel = transform.Find("AudioPanel")?.gameObject;
        if (ControlsPanel == null) ControlsPanel = transform.Find("ControlsPanel")?.gameObject;
        if (UIAndAccessibilityPanel == null) UIAndAccessibilityPanel = transform.Find("UIAndAccessibilityPanel")?.gameObject;

        if (GamePlayButton == null) GamePlayButton = transform.Find("SettingSelectPanel/GamePlayButton")?.GetComponent<Button>();
        if (GraphicsButton == null) GraphicsButton = transform.Find("SettingSelectPanel/GraphicsButton")?.GetComponent<Button>();
        if (AudioButton == null) AudioButton = transform.Find("SettingSelectPanel/AudioButton")?.GetComponent<Button>();
        if (ControlsButton == null) ControlsButton = transform.Find("SettingSelectPanel/ControlsButton")?.GetComponent<Button>();
        if (UIAndAccessibilityButton == null) UIAndAccessibilityButton = transform.Find("SettingSelectPanel/UIAndAccessibilityButton")?.GetComponent<Button>();

        InGameBackGroundColor = gameObject.GetComponent<Image>().color;
    }
    public void Init()
    {
        //버튼들 AddListener 달아주기
        GamePlayButton.onClick.AddListener(OnClickGamePlay);
        GraphicsButton.onClick.AddListener(OnClickGraphics);
        AudioButton.onClick.AddListener(OnClickAudio);
        ControlsButton.onClick.AddListener(OnClickControls);
        UIAndAccessibilityButton.onClick.AddListener(OnClickUIAndAccessibility);
    }

    //어디선가 Setting 패널을 열었을 때
    public void SettingOpen(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, SettingSelectPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext);

        //이전 컨텍스트가 MainMenu였을 경우 배경 투명도 반투명
        if(beforeContext == InputContext.MainMenu)
        {
            gameObject.GetComponent<Image>().color = MainMenuBackGroundColor;
        }
        //이전 컨텍스트가 MainMenu가 아닐 경우 배경 검은색
        else
        {
            gameObject.GetComponent<Image>().color = InGameBackGroundColor;
        }
    }

    ///<Input>
    public void ESC(bool esc)
    {
        if(panelStack.Count > 0)
        {
            //뒤로가기
            UIPanelController.Back(panelStack, ref currentPanel);
        }
        else
        {
            //닫기
            UIPanelController.Close(ref currentPanel, gameObject);
            if (beforeContext == InputContext.SystemMenu)
            {
                InputEvents.SystemMenu.InvokeSystemMenuOpen(thisContext);
            }
            else if (beforeContext == InputContext.MainMenu)
            {
                InputEvents.MainMenu.InvokeMainMenuOpen(thisContext);
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

    void OnClickGamePlay()
    {
        UIPanelController.OpenPanel(panelStack, ref currentPanel, GamePlayPanel, gameObject);
    }
    void OnClickGraphics()
    {
        UIPanelController.OpenPanel(panelStack, ref currentPanel, GraphicsPanel, gameObject);
    }
    void OnClickAudio()
    {
        UIPanelController.OpenPanel(panelStack, ref currentPanel, AudioPanel, gameObject);
    }
    void OnClickControls()
    {
        UIPanelController.OpenPanel(panelStack, ref currentPanel, ControlsPanel, gameObject);
    }
    void OnClickUIAndAccessibility()
    {
        UIPanelController.OpenPanel(panelStack, ref currentPanel, UIAndAccessibilityPanel, gameObject);
    }
}
