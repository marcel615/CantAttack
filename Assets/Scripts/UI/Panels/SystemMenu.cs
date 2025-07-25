using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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

    //SystemMenu 조작 관련 변수
    bool isOpen;

    //현재 선택된 UI 객체(버튼 등)
    GameObject selected;


    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (SystemMenuSelectPanel == null) SystemMenuSelectPanel = transform.Find("SystemMenuSelectPanel")?.gameObject;
        if (ContinueButton == null) ContinueButton = transform.Find("SystemMenuSelectPanel/ContinueButton")?.GetComponent<Button>();
        if (SettingButton == null) SettingButton = transform.Find("SystemMenuSelectPanel/SettingButton")?.GetComponent<Button>();
        if (SaveAndExitButton == null) SaveAndExitButton = transform.Find("SystemMenuSelectPanel/SaveAndExitButton")?.GetComponent<Button>();

    }

    private void Start()
    {
        ContinueButton.onClick.AddListener(OnClickContinue);
        SettingButton.onClick.AddListener(OnClickSetting);
        SaveAndExitButton.onClick.AddListener(OnClickSaveAndExit);
    }



    public void ESC(bool esc)
    {
        if (!isOpen)
        {
            Open();
            InputEvents.InvokeContextUpdate(thisContext, true);
        }
        else
        {
            Close();
            InputEvents.InvokeContextUpdate(thisContext, false);
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
        SystemMenuSelectPanel.SetActive(true);
        isOpen = true;

        InputEvents.InvokeSelectFirstSelectable(SystemMenuSelectPanel);
    }
    void Close()
    {
        gameObject.SetActive(false);
        SystemMenuSelectPanel.SetActive(false);
        isOpen = false;
    }

    void OnClickContinue()
    {
        Close();
        InputEvents.InvokeContextUpdate(thisContext, false);
    }
    void OnClickSetting()
    {
        Close();
        InputEvents.InvokeContextUpdate(thisContext, false);
        InputEvents.InvokeContextUpdate(InputContext.Setting, true);
        InputEvents.Setting.InvokeSettingOpen();

    }
    void OnClickSaveAndExit()
    {

    }


}
