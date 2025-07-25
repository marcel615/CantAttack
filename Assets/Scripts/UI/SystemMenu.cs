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
    public InputContext systemMenuContext = InputContext.SystemMenu;

    //SystemMenu 조작 관련 변수
    bool isOpen;


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
            InputEvents.InvokeContextUpdate(systemMenuContext, true);
        }
        else
        {
            Close();
            InputEvents.InvokeContextUpdate(systemMenuContext, false);
        }

    }
    public void Enter(bool enter)
    {

    }
    public void E(bool e)
    {

    }

    void Open()
    {
        gameObject.SetActive(true);
        SystemMenuSelectPanel.SetActive(true);
        isOpen = true;
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
        InputEvents.InvokeContextUpdate(InputContext.SystemMenu, false);
    }
    void OnClickSetting()
    {
        Close();
        InputEvents.InvokeContextUpdate(InputContext.SystemMenu, false);
        InputEvents.InvokeContextUpdate(InputContext.Setting, true);

    }
    void OnClickSaveAndExit()
    {

    }


}
