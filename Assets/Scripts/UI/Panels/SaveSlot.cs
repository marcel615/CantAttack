using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private GameObject SaveSlotSelectPanel;
    [SerializeField] private GameObject ActionHintPanel;
    [SerializeField] private GameObject SaveSlotScrollViewPanel;
    [SerializeField] private GameObject ContentPanel;

    [SerializeField] private Button SaveSlot1;
    [SerializeField] private Button SaveSlot2;
    [SerializeField] private Button SaveSlot3;
    [SerializeField] private Button SaveSlot4;

    //SaveSlot 프리팹
    [SerializeField] private GameObject SlotPrefab;
    private List<SaveSlotPrefab> slotList = new List<SaveSlotPrefab>();
    int slotCount = 30;

    //컨텍스트 enum 정보
    public InputContext thisContext = InputContext.SaveSlot;
    public InputContext beforeContext;

    //SystemMenu 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;

    //테스트인지 확인 
    bool isTestSave;
    //세이브&로드 Path 관련
    string TestSavePath => Path.Combine(Application.dataPath, "TestSaveFolder");  //테스트 경로
    string RealSavePath => Application.persistentDataPath;                        //실제 경로

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (SaveSlotSelectPanel == null) SaveSlotSelectPanel = transform.Find("SaveSlotSelectPanel")?.gameObject;
        if (ActionHintPanel == null) ActionHintPanel = transform.Find("ActionHintPanel")?.gameObject;
        if (SaveSlotScrollViewPanel == null) SaveSlotScrollViewPanel = transform.Find("SaveSlotScrollViewPanel")?.gameObject;
        if (ContentPanel == null) ContentPanel = transform.Find("SaveSlotScrollViewPanel/Viewport/ContentPanel")?.gameObject;

        if (SaveSlot1 == null) SaveSlot1 = transform.Find("SaveSlotSelectPanel/SaveSlot1")?.GetComponent<Button>();
        if (SaveSlot2 == null) SaveSlot2 = transform.Find("SaveSlotSelectPanel/SaveSlot2")?.GetComponent<Button>();
        if (SaveSlot3 == null) SaveSlot3 = transform.Find("SaveSlotSelectPanel/SaveSlot3")?.GetComponent<Button>();
        if (SaveSlot4 == null) SaveSlot4 = transform.Find("SaveSlotSelectPanel/SaveSlot4")?.GetComponent<Button>();
        
        //SaveSlot 생성
        InitSaveSlotPrefab();
    }

    //이벤트 구독
    private void OnEnable()
    {
        //세이브 로드 시작하면
        SystemEvents.OnDataLoadStart += LoadStart;

    }
    private void OnDisable()
    {
        //세이브 로드 시작하면
        SystemEvents.OnDataLoadStart -= LoadStart;

    }
    //세이브 로드 시작하면
    void LoadStart(int whatever)
    {
        UIPanelController.Close(ref currentPanel, gameObject, thisContext);
        InputEvents.InvokeContextUpdate(InputContext.Player, true);

    }

    //어디선가 SaveSlot 패널을 열었을 때
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
            //뒤로가기
            UIPanelController.Back(panelStack, ref currentPanel);
        }
        else
        {
            //닫기
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
    //SaveSlot 생성하는 메소드
    void InitSaveSlotPrefab()
    {
        for (int i = 1; i < slotCount + 1; i++)
        {
            GameObject slotGameObject = Instantiate(SlotPrefab, ContentPanel.transform);
            SaveSlotPrefab saveSlotPrefab = slotGameObject.GetComponent<SaveSlotPrefab>();
            slotList.Add(saveSlotPrefab);

            //Test인지 확인 후 세이브파일 경로 알아낸 뒤 SaveSlot.Init() 실행
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
    //SaveSlot의 보이는 정보 업데이트 메소드
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
