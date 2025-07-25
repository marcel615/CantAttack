using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private GameObject SettingSelectPanel;
    [SerializeField] private Button GamePlayButton;
    [SerializeField] private Button GraphicsButton;
    [SerializeField] private Button AudioButton;
    [SerializeField] private Button ControlsButton;
    [SerializeField] private Button UIAndAccessibilityButton;

    //컨텍스트 enum 정보
    public InputContext settingContext = InputContext.Setting;

    //Setting 조작 관련 변수
    bool isOpen;

    //현재 선택된 UI 객체(버튼 등)
    GameObject selected;


    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
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
        //포커싱된 오브젝트 할당
        selected = EventSystem.current.currentSelectedGameObject;
        Button selectedButton = selected.GetComponent<Button>();

        //포커싱된 오브젝트 클릭
        if (selectedButton != null)
        {
            selectedButton.onClick.Invoke();
        }
        //포커싱된 오브젝트 해제
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
