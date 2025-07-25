using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SystemMenu : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private GameObject SystemMenuSelectPanel;
    [SerializeField] private Button ContinueButton;
    [SerializeField] private Button SettingButton;
    [SerializeField] private Button SaveAndExitButton;

    //���ؽ�Ʈ enum ����
    public InputContext thisContext = InputContext.SystemMenu;

    //SystemMenu ���� ���� ����
    bool isOpen;

    //���� ���õ� UI ��ü(��ư ��)
    GameObject selected;


    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
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
