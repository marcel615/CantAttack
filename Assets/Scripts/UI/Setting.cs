using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private GameObject SettingSelectPanel;
    [SerializeField] private Button GamePlayButton;
    [SerializeField] private Button GraphicsButton;
    [SerializeField] private Button AudioButton;
    [SerializeField] private Button ControlsButton;
    [SerializeField] private Button UIAndAccessibilityButton;

    //���ؽ�Ʈ enum ����
    public InputContext settingContext = InputContext.Setting;

    //Setting ���� ���� ����
    bool isOpen;

    //���� ���õ� UI ��ü(��ư ��)
    GameObject selected;


    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (SettingSelectPanel == null) SettingSelectPanel = transform.Find("SettingSelectPanel")?.gameObject;
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



    public void ESC(bool esc)
    {
        if (!isOpen)
        {
            Open();
            InputEvents.InvokeContextUpdate(settingContext, true);
        }
        else
        {
            Close();
            InputEvents.InvokeContextUpdate(settingContext, false);
        }

    }
    public void Enter(bool enter)
    {

    }
    public void E(bool e)
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

    void Open()
    {
        gameObject.SetActive(true);
        SettingSelectPanel.SetActive(true);
        isOpen = true;
    }
    void Close()
    {
        gameObject.SetActive(false);
        SettingSelectPanel.SetActive(false);
        isOpen = false;
    }

    void OnClickGamePlay()
    {
        Close();
        InputEvents.InvokeContextUpdate(InputContext.SystemMenu, false);
    }
    void OnClickGraphics()
    {

    }
    void OnClickAudio()
    {

    }
    void OnClickControls()
    {

    }
    void OnClickUIAndAccessibility()
    {

    }
}
