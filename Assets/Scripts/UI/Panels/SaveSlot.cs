using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private GameObject SaveSlotSelectPanel;
    [SerializeField] private GameObject ActionHintPanel;

    [SerializeField] private Button SaveSlot1;
    [SerializeField] private Button SaveSlot2;
    [SerializeField] private Button SaveSlot3;
    [SerializeField] private Button SaveSlot4;

    //���ؽ�Ʈ enum ����
    public InputContext thisContext = InputContext.SaveSlot;
    public InputContext beforeContext;

    //SystemMenu ���� ���� ����
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;


    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (SaveSlotSelectPanel == null) SaveSlotSelectPanel = transform.Find("SaveSlotSelectPanel")?.gameObject;
        if (ActionHintPanel == null) ActionHintPanel = transform.Find("ActionHintPanel")?.gameObject;

        if (SaveSlot1 == null) SaveSlot1 = transform.Find("SaveSlotSelectPanel/SaveSlot1")?.GetComponent<Button>();
        if (SaveSlot2 == null) SaveSlot2 = transform.Find("SaveSlotSelectPanel/SaveSlot2")?.GetComponent<Button>();
        if (SaveSlot3 == null) SaveSlot3 = transform.Find("SaveSlotSelectPanel/SaveSlot3")?.GetComponent<Button>();
        if (SaveSlot4 == null) SaveSlot4 = transform.Find("SaveSlotSelectPanel/SaveSlot4")?.GetComponent<Button>();

    }

    private void Start()
    {
        SaveSlot1.onClick.AddListener(OnSaveSlot1);
        SaveSlot2.onClick.AddListener(OnSaveSlot2);
        SaveSlot3.onClick.AddListener(OnSaveSlot3);
        SaveSlot4.onClick.AddListener(OnSaveSlot4);
    }
    //�̺�Ʈ ����
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }

    //��𼱰� SaveSlot �г��� ������ ��
    public void SaveSlotOpen(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, SaveSlotSelectPanel, gameObject);
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
            UIPanelController.Close(ref currentPanel, gameObject, thisContext);
            if (beforeContext == InputContext.MainMenu)
            {
                InputEvents.InvokeContextUpdate(InputContext.MainMenu, true);
                InputEvents.MainMenu.InvokeMainMenuOpen(thisContext);
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

    void OnSaveSlot1()
    {
        UIPanelController.Close(ref currentPanel, gameObject, thisContext);
        //InputEvents.InvokeContextUpdate(InputContext.Player, true);
    }
    void OnSaveSlot2()
    {
        UIPanelController.Close(ref currentPanel, gameObject, thisContext);
        //InputEvents.InvokeContextUpdate(InputContext.Player, true);
    }
    void OnSaveSlot3()
    {
        UIPanelController.Close(ref currentPanel, gameObject, thisContext);
        //InputEvents.InvokeContextUpdate(InputContext.Setting, true);
        //InputEvents.Setting.InvokeSettingOpen();
    }
    void OnSaveSlot4()
    {

    }

}
