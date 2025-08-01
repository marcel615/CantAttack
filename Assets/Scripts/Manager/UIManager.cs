using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static UIManager Instance;

    //자식 오브젝트 (UI 오브젝트)
    //Canvas
    [SerializeField] private GameObject UICanvas;
    //PlayerHUDPanel
    [SerializeField] private GameObject PlayerHUDPanel;
    //SystemMenuPanel
    [SerializeField] private GameObject SystemMenuPanel;
    //SettingPanel
    [SerializeField] private GameObject SettingPanel;
    //MainMenuPanel
    [SerializeField] private GameObject MainMenuPanel;
    //SaveSlotPanel
    [SerializeField] private GameObject SaveSlotPanel;
    //TutorialPanel
    [SerializeField] private GameObject TutorialPanel;
    //DialoguePanel
    [SerializeField] private GameObject DialoguePanel;
    //FadePanel
    [SerializeField] private GameObject FadePanel;




    private void Awake()
    {
        // 기존 인스턴스가 존재할 때 && 지금 새로운 인스턴스가 생성되려고 할 때
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //중복되지 않도록 지금 새롭게 생성되는 놈은 파괴시킨다
            return;
        }
        // 인스턴스 처음 할당
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (UICanvas == null) UICanvas = transform.Find("UICanvas")?.gameObject;

        if (PlayerHUDPanel == null) PlayerHUDPanel = transform.Find("UICanvas/PlayerHUDPanel")?.gameObject;
        if (SystemMenuPanel == null) SystemMenuPanel = transform.Find("UICanvas/SystemMenuPanel")?.gameObject;
        if (SettingPanel == null) SettingPanel = transform.Find("UICanvas/SettingPanel")?.gameObject;
        if (MainMenuPanel == null) MainMenuPanel = transform.Find("UICanvas/MainMenuPanel")?.gameObject;
        if (SaveSlotPanel == null) SaveSlotPanel = transform.Find("UICanvas/SaveSlotPanel")?.gameObject;
        if (TutorialPanel == null) TutorialPanel = transform.Find("UICanvas/TutorialPanel")?.gameObject;
        if (DialoguePanel == null) DialoguePanel = transform.Find("UICanvas/DialoguePanel")?.gameObject;
        if (FadePanel == null) FadePanel = transform.Find("UICanvas/FadePanel")?.gameObject;

        //검은 화면부터 시작되도록
        MainMenuPanel.SetActive(true);
        FadePanel.SetActive(true);
    }
    public void Init()
    {
        SystemMenuPanel.GetComponent<SystemMenu>().Init();
        SettingPanel.GetComponent<Setting>().Init();
        MainMenuPanel.GetComponent<MainMenu>().Init();
    }

}
