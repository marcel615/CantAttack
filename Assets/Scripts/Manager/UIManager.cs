using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    public static UIManager Instance;

    //�ڽ� ������Ʈ (UI ������Ʈ)
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
        // ���� �ν��Ͻ��� ������ �� && ���� ���ο� �ν��Ͻ��� �����Ƿ��� �� ��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //�ߺ����� �ʵ��� ���� ���Ӱ� �����Ǵ� ���� �ı���Ų��
            return;
        }
        // �ν��Ͻ� ó�� �Ҵ�
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (UICanvas == null) UICanvas = transform.Find("UICanvas")?.gameObject;

        if (PlayerHUDPanel == null) PlayerHUDPanel = transform.Find("UICanvas/PlayerHUDPanel")?.gameObject;
        if (SystemMenuPanel == null) SystemMenuPanel = transform.Find("UICanvas/SystemMenuPanel")?.gameObject;
        if (SettingPanel == null) SettingPanel = transform.Find("UICanvas/SettingPanel")?.gameObject;
        if (MainMenuPanel == null) MainMenuPanel = transform.Find("UICanvas/MainMenuPanel")?.gameObject;
        if (SaveSlotPanel == null) SaveSlotPanel = transform.Find("UICanvas/SaveSlotPanel")?.gameObject;
        if (TutorialPanel == null) TutorialPanel = transform.Find("UICanvas/TutorialPanel")?.gameObject;
        if (DialoguePanel == null) DialoguePanel = transform.Find("UICanvas/DialoguePanel")?.gameObject;
        if (FadePanel == null) FadePanel = transform.Find("UICanvas/FadePanel")?.gameObject;

        //���� ȭ����� ���۵ǵ���
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
