using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private GameObject GameTitlePanel;
    [SerializeField] private GameObject MainMenuSelectPanel;
    [SerializeField] private GameObject InfoPanel;

    [SerializeField] private Button ContinueButton;
    [SerializeField] private Button LoadGameButton;
    [SerializeField] private Button SettingButton;
    [SerializeField] private Button QuitButton;

    //���ؽ�Ʈ enum ����
    public InputContext thisContext = InputContext.MainMenu;
    public InputContext beforeContext;

    //MainMenu ���� ���� ����
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;


    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (GameTitlePanel == null) GameTitlePanel = transform.Find("GameTitlePanel")?.gameObject;
        if (MainMenuSelectPanel == null) MainMenuSelectPanel = transform.Find("MainMenuSelectPanel")?.gameObject;
        if (InfoPanel == null) InfoPanel = transform.Find("InfoPanel")?.gameObject;

        if (ContinueButton == null) ContinueButton = transform.Find("MainMenuSelectPanel/ContinueButton")?.GetComponent<Button>();
        if (LoadGameButton == null) LoadGameButton = transform.Find("MainMenuSelectPanel/LoadGameButton")?.GetComponent<Button>();
        if (SettingButton == null) SettingButton = transform.Find("MainMenuSelectPanel/SettingButton")?.GetComponent<Button>();
        if (QuitButton == null) QuitButton = transform.Find("MainMenuSelectPanel/QuitButton")?.GetComponent<Button>();

        //�ʱ�ȭ �۾�
        SelectFirstButton();
    }
    public void Init()
    {
        //��ư�� AddListener �޾��ֱ�
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

    //��𼱰� MainMenu �г��� ������ ��
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
            //�ڷΰ���
            UIPanelController.Back(panelStack, ref currentPanel);
        }
        else
        {
            //�ݱ�
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
