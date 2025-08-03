using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SystemMenu : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private GameObject SystemMenuSelectPanel;
    [SerializeField] private Button ContinueButton;
    [SerializeField] private Button SettingButton;
    [SerializeField] private Button SaveAndExitButton;

    //���ؽ�Ʈ enum ����
    InputContext thisContext = InputContext.SystemMenu;
    InputContext beforeContext;

    //SystemMenu ���� ���� ����
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;


    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (SystemMenuSelectPanel == null) SystemMenuSelectPanel = transform.Find("SystemMenuSelectPanel")?.gameObject;
        if (ContinueButton == null) ContinueButton = transform.Find("SystemMenuSelectPanel/ContinueButton")?.GetComponent<Button>();
        if (SettingButton == null) SettingButton = transform.Find("SystemMenuSelectPanel/SettingButton")?.GetComponent<Button>();
        if (SaveAndExitButton == null) SaveAndExitButton = transform.Find("SystemMenuSelectPanel/SaveAndExitButton")?.GetComponent<Button>();

    }
    public void Init()
    {
        //��ư�� AddListener �޾��ֱ�
        ContinueButton.onClick.AddListener(OnClickContinue);
        SettingButton.onClick.AddListener(OnClickSetting);
        SaveAndExitButton.onClick.AddListener(OnClickSaveAndExit);
    }

    //��𼱰� SystemMenu �г��� ������ ��
    public void SystemMenuOpen(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, SystemMenuSelectPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext);

        //���� �ð� ���ߵ��� �̺�Ʈ ����
        SystemEvents.InvokeChangeTimeScale(0f);
    }
    //��𼱰� SystemMenu �г��� �ݾ��� ��
    public void SystemMenuClose(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        //�ݱ�
        UIPanelController.Close(ref currentPanel, gameObject);
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
            UIPanelController.Close(ref currentPanel, gameObject);
            InputEvents.InvokeContextUpdate(InputContext.Player);
            //���� �ð� �ٽ� �귯������ �̺�Ʈ ����
            SystemEvents.InvokeChangeTimeScale(1f);
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
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(InputContext.Player);
        //���� �ð� �ٽ� �귯������ �̺�Ʈ ����
        SystemEvents.InvokeChangeTimeScale(1f);
    }
    void OnClickSetting()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.Setting.InvokeSettingOpen(thisContext);
    }
    void OnClickSaveAndExit()
    {
        //���� �ð� �ٽ� �귯������ �̺�Ʈ ����
        SystemEvents.InvokeChangeTimeScale(1f);
        //�� ��ȯ ����
        SceneTransitionEvents.InvokeSystemMenuToMainMenu("MainMenu");
    }


}
