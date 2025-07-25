using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    //�ڽ� ������Ʈ
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

    //���ؽ�Ʈ enum ����
    public InputContext thisContext = InputContext.Setting;

    //Setting ���� ���� ����
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //���� ���õ� UI ��ü(��ư ��)
    GameObject selected;


    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
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

    }

    private void Start()
    {
        GamePlayButton.onClick.AddListener(OnClickGamePlay);
        GraphicsButton.onClick.AddListener(OnClickGraphics);
        AudioButton.onClick.AddListener(OnClickAudio);
        ControlsButton.onClick.AddListener(OnClickControls);
        UIAndAccessibilityButton.onClick.AddListener(OnClickUIAndAccessibility);
    }

    public void SettingOpen()
    {
        OpenPanel(SettingSelectPanel);
        InputEvents.InvokeContextUpdate(thisContext, true);

    }
    ///<Input>
    public void ESC(bool esc)
    {
        if(panelStack.Count > 0)
        {
            Back();
        }
        else
        {
            Close();
            InputEvents.InvokeContextUpdate(InputContext.Player, true);
        }
    }
    public void Enter(bool enter)
    {

    }
    public void E(bool e)
    {
        TriggerSelectAction();
    }
    /// </Input>

    void OpenPanel(GameObject newPanel)
    {
        if(currentPanel != null)
        {
            panelStack.Push(currentPanel);
            currentPanel.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        currentPanel = newPanel;
        currentPanel.SetActive(true);
        InputEvents.InvokeSelectFirstSelectable(currentPanel);

    }
    void Back()
    {
        currentPanel.SetActive(false);
        currentPanel = panelStack.Pop();
        currentPanel.SetActive(true);
        InputEvents.InvokeSelectFirstSelectable(currentPanel);
    }
    void Close()
    {
        currentPanel.SetActive(false);
        gameObject.SetActive(false);
        InputEvents.InvokeContextUpdate(thisContext, false);
    }
    void TriggerSelectAction()
    {
        //��Ŀ�̵� ������Ʈ �Ҵ�
        selected = EventSystem.current.currentSelectedGameObject;
        Button selectedButton = selected.GetComponent<Button>();

        //��Ŀ�̵� ������Ʈ Ŭ��
        if (selectedButton != null)
        {
            selectedButton.onClick.Invoke();
        }
        //��Ŀ�̵� ������Ʈ ����
        selected = null;
        selectedButton = null;
    }


    void OnClickGamePlay()
    {
        OpenPanel(GamePlayPanel);
    }
    void OnClickGraphics()
    {
        OpenPanel(GraphicsPanel);
    }
    void OnClickAudio()
    {
        OpenPanel(AudioPanel);
    }
    void OnClickControls()
    {
        OpenPanel(ControlsPanel);
    }
    void OnClickUIAndAccessibility()
    {
        OpenPanel(UIAndAccessibilityPanel);
    }
}
