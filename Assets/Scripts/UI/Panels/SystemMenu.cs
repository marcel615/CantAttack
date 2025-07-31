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
    public InputContext thisContext = InputContext.SystemMenu;
    public InputContext beforeContext;

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

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //OnClickSaveAndExit ���� ���̺� �Ϸ�Ǹ� ����Ǵ� �̺�Ʈ ����
        SystemEvents.OnSaveEnd += SaveEnd;
    }
    private void OnDisable()
    {
        //OnClickSaveAndExit ���� ���̺� �Ϸ�Ǹ� ����Ǵ� �̺�Ʈ ����
        SystemEvents.OnSaveEnd -= SaveEnd;
    }

    //��𼱰� SystemMenu �г��� ������ ��
    public void SystemMenuOpen(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, SystemMenuSelectPanel, gameObject);
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
            UIPanelController.Close(ref currentPanel, gameObject);
            InputEvents.InvokeContextUpdate(thisContext, false);
            if (beforeContext == InputContext.Player)
            {
                InputEvents.InvokeContextUpdate(InputContext.Player, true);
            }
            if (beforeContext == InputContext.Setting)
            {
                InputEvents.InvokeContextUpdate(InputContext.Player, true);
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

    void OnClickContinue()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext, false);
        InputEvents.InvokeContextUpdate(InputContext.Player, true);
    }
    void OnClickSetting()
    {
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext, false);
        InputEvents.InvokeContextUpdate(InputContext.Setting, true);
        InputEvents.Setting.InvokeSettingOpen(thisContext);
    }
    void OnClickSaveAndExit()
    {
        SystemEvents.InvokeSaveRequest();
    }
    //OnClickSaveAndExit ���� ���̺� �Ϸ�Ǹ� ����Ǵ� �̺�Ʈ ����
    void SaveEnd()
    {
        //���� UI �ݰ� ���θ޴��� ������
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(thisContext, false);
        InputEvents.InvokeContextUpdate(InputContext.MainMenu, true);
        InputEvents.MainMenu.InvokeMainMenuOpen(thisContext);
        // ���θ޴� �� �ε�
        SceneManager.LoadScene("MainMenu");
    }


}
