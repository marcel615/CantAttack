using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParryMode : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private Button Top;
    [SerializeField] private GameObject TopContainer;
    [SerializeField] private Button Right;
    [SerializeField] private GameObject RightContainer;
    [SerializeField] private Button Bottom;
    [SerializeField] private GameObject BottomContainer;
    [SerializeField] private Button Left;
    [SerializeField] private GameObject LeftContainer;

    //컨텍스트 enum 정보
    InputContext thisContext = InputContext.ParryMode;
    InputContext beforeContext;

    //ParryMode 조작 관련 변수
    Stack<GameObject> panelStack = new Stack<GameObject>();
    GameObject currentPanel;


    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (Top == null) Top = transform.Find("Top")?.GetComponent<Button>();
        if (TopContainer == null) TopContainer = transform.Find("Top/ZeroRotationContainer").gameObject;
        if (Right == null) Right = transform.Find("Right")?.GetComponent<Button>();
        if (RightContainer == null) RightContainer = transform.Find("Right/ZeroRotationContainer").gameObject;
        if (Bottom == null) Bottom = transform.Find("Bottom")?.GetComponent<Button>();
        if (BottomContainer == null) BottomContainer = transform.Find("Bottom/ZeroRotationContainer").gameObject;
        if (Left == null) Left = transform.Find("Left")?.GetComponent<Button>();
        if (LeftContainer == null) LeftContainer = transform.Find("Left/ZeroRotationContainer").gameObject;
    }
    public void Init()
    {
        //버튼들 AddListener 달아주기
        Top.onClick.AddListener(OnClickedTop);
        Right.onClick.AddListener(OnClickedRight);
        Bottom.onClick.AddListener(OnClickedBottom);
        Left.onClick.AddListener(OnClickedLeft);
    }

    //어디선가 ParryMode 패널을 열었을 때
    public void ParryModeOpen(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        UIPanelController.OpenPanel(panelStack, ref currentPanel, gameObject, gameObject);
        InputEvents.InvokeContextUpdate(thisContext);

        //게임 시간 멈추도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(0f);
    }
    //어디선가 ParryMode 패널을 닫았을 때
    public void ParryModeClose(InputContext sourceInputContext)
    {
        beforeContext = sourceInputContext;
        //닫기
        UIPanelController.Close(ref currentPanel, gameObject);
    }

    ///<Input>
    public void Tab(bool tab)
    {
        if (panelStack.Count > 0)
        {
            //뒤로가기
            UIPanelController.Back(panelStack, ref currentPanel);
        }
        else
        {
            //닫기
            UIPanelController.Close(ref currentPanel, gameObject);
            InputEvents.InvokeContextUpdate(InputContext.Player);
            //게임 시간 다시 흘러가도록 이벤트 발행
            SystemEvents.InvokeChangeTimeScale(1f);
        }
    }
    /// </Input>

    void OnClickedTop()
    {
        Debug.Log("Top Selected");
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(InputContext.Player);
        //게임 시간 다시 흘러가도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(1f);
    }
    void OnClickedRight()
    {
        Debug.Log("Right Selected");
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(InputContext.Player);
        //게임 시간 다시 흘러가도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(1f);
    }
    void OnClickedBottom()
    {
        Debug.Log("Bottom Selected");
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(InputContext.Player);
        //게임 시간 다시 흘러가도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(1f);
    }
    void OnClickedLeft()
    {
        Debug.Log("Left Selected");
        UIPanelController.Close(ref currentPanel, gameObject);
        InputEvents.InvokeContextUpdate(InputContext.Player);
        //게임 시간 다시 흘러가도록 이벤트 발행
        SystemEvents.InvokeChangeTimeScale(1f);
    }


}
