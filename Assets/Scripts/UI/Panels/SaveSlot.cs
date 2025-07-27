using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private GameObject SaveSlotSelectPanel;
    [SerializeField] private GameObject ActionHintPanel;
    [SerializeField] private GameObject SaveSlotScrollViewPanel;
    [SerializeField] private GameObject ContentPanel;

    [SerializeField] private Button SaveSlot1;
    [SerializeField] private Button SaveSlot2;
    [SerializeField] private Button SaveSlot3;
    [SerializeField] private Button SaveSlot4;

    //SaveSlot ������
    [SerializeField] private GameObject SlotPrefab;
    private List<SaveSlotPrefab> slotList = new List<SaveSlotPrefab>();
    int slotCount = 30;

    //���ؽ�Ʈ enum ����
    public InputContext thisContext = InputContext.SaveSlot;
    public InputContext beforeContext;

    //SystemMenu ���� ���� ����
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //�׽�Ʈ���� Ȯ�� 
    bool isTestSave;
    //���̺�&�ε� Path ����
    string TestSavePath => Path.Combine(Application.dataPath, "TestSaveFolder");  //�׽�Ʈ ���
    string RealSavePath => Application.persistentDataPath;                        //���� ���

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (SaveSlotSelectPanel == null) SaveSlotSelectPanel = transform.Find("SaveSlotSelectPanel")?.gameObject;
        if (ActionHintPanel == null) ActionHintPanel = transform.Find("ActionHintPanel")?.gameObject;
        if (SaveSlotScrollViewPanel == null) SaveSlotScrollViewPanel = transform.Find("SaveSlotScrollViewPanel")?.gameObject;
        if (ContentPanel == null) ContentPanel = transform.Find("SaveSlotScrollViewPanel/Viewport/ContentPanel")?.gameObject;

        if (SaveSlot1 == null) SaveSlot1 = transform.Find("SaveSlotSelectPanel/SaveSlot1")?.GetComponent<Button>();
        if (SaveSlot2 == null) SaveSlot2 = transform.Find("SaveSlotSelectPanel/SaveSlot2")?.GetComponent<Button>();
        if (SaveSlot3 == null) SaveSlot3 = transform.Find("SaveSlotSelectPanel/SaveSlot3")?.GetComponent<Button>();
        if (SaveSlot4 == null) SaveSlot4 = transform.Find("SaveSlotSelectPanel/SaveSlot4")?.GetComponent<Button>();
        
        //SaveSlot ����
        InitSaveSlotPrefab();
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //���̺� �ε� �����ϸ�
        SystemEvents.OnDataLoadStart += LoadStart;

    }
    private void OnDisable()
    {
        //���̺� �ε� �����ϸ�
        SystemEvents.OnDataLoadStart -= LoadStart;

    }
    //���̺� �ε� �����ϸ�
    void LoadStart(int whatever)
    {
        UIPanelController.Close(ref currentPanel, gameObject, thisContext);
        InputEvents.InvokeContextUpdate(InputContext.Player, true);

    }

    //��𼱰� SaveSlot �г��� ������ ��
    public void SaveSlotOpen(InputContext sourceInputContext)
    {
        UpdateSaveSlot();

        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, ContentPanel, gameObject);
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
    //SaveSlot �����ϴ� �޼ҵ�
    void InitSaveSlotPrefab()
    {
        for (int i = 1; i < slotCount + 1; i++)
        {
            GameObject slotGameObject = Instantiate(SlotPrefab, ContentPanel.transform);
            SaveSlotPrefab saveSlotPrefab = slotGameObject.GetComponent<SaveSlotPrefab>();
            slotList.Add(saveSlotPrefab);

            //Test���� Ȯ�� �� ���̺����� ��� �˾Ƴ� �� SaveSlot.Init() ����
            isTestSave = GameManager.Instance.isTest;
            string fileName;
            string filePath;
            if (isTestSave)
            {
                fileName = $"TestSaveFile{i}.json";
                filePath = Path.Combine(TestSavePath, fileName);
            }
            else
            {
                fileName = $"SaveFile{i}.json";
                filePath = Path.Combine(RealSavePath, fileName);
            }
            saveSlotPrefab.Init(i, filePath);

        }
    }
    //SaveSlot�� ���̴� ���� ������Ʈ �޼ҵ�
    void UpdateSaveSlot()
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i] != null)
            {
                slotList[i].SetSaveSlot();
            }
        }
    }

}
