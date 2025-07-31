using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private GameObject GameTitlePanel;
    [SerializeField] private GameObject MainMenuSelectPanel;
    [SerializeField] private GameObject InfoPanel;

    [SerializeField] private Button ContinueButton;
    [SerializeField] private Button LoadGameButton;
    [SerializeField] private Button SettingButton;
    [SerializeField] private Button QuitButton;

    //컨텍스트 enum 정보
    public InputContext thisContext = InputContext.MainMenu;
    public InputContext beforeContext;

    //MainMenu 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;


    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (GameTitlePanel == null) GameTitlePanel = transform.Find("GameTitlePanel")?.gameObject;
        if (MainMenuSelectPanel == null) MainMenuSelectPanel = transform.Find("MainMenuSelectPanel")?.gameObject;
        if (InfoPanel == null) InfoPanel = transform.Find("InfoPanel")?.gameObject;

        if (ContinueButton == null) ContinueButton = transform.Find("MainMenuSelectPanel/ContinueButton")?.GetComponent<Button>();
        if (LoadGameButton == null) LoadGameButton = transform.Find("MainMenuSelectPanel/LoadGameButton")?.GetComponent<Button>();
        if (SettingButton == null) SettingButton = transform.Find("MainMenuSelectPanel/SettingButton")?.GetComponent<Button>();
        if (QuitButton == null) QuitButton = transform.Find("MainMenuSelectPanel/QuitButton")?.GetComponent<Button>();

        //초기화 작업
        SelectFirstButton();
    }
    public void Init()
    {
        //버튼들 AddListener 달아주기
        ContinueButton.onClick.AddListener(OnClickContinue);
        LoadGameButton.onClick.AddListener(OnClickLoadGame);
        SettingButton.onClick.AddListener(OnClickSetting);
        QuitButton.onClick.AddListener(OnClickQuit);
    }

    void SelectFirstButton()
    {
        EventSystem.current.SetSelectedGameObject(null);

        Selectable firstSelectable = MainMenuSelectPanel.GetComponentInChildren<Selectable>();
        if (firstSelectable != null)
        {
            firstSelectable.Select();
        }
        UIPanelController.OpenPanel(panelStack, ref currentPanel, MainMenuSelectPanel, gameObject);

    }

    //어디선가 MainMenu 패널을 열었을 때
    public void MainMenuOpen(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, MainMenuSelectPanel, gameObject);
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
        SystemEvents.InvokeNewGameORLatestSave();

        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext, false);
        InputEvents.InvokeContextUpdate(InputContext.Player, true);
    }
    void OnClickLoadGame()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext, false);
        InputEvents.InvokeContextUpdate(InputContext.SaveSlot, true);
        InputEvents.SaveSlot.InvokeSaveSlotOpen(thisContext);

    }
    void OnClickSetting()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext, false);
        InputEvents.InvokeContextUpdate(InputContext.Setting, true);
        InputEvents.Setting.InvokeSettingOpen(thisContext);
    }
    void OnClickQuit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

}
