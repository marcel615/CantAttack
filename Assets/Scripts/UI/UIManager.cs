using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    private static UIManager Instance;

    //�ڽ� ������Ʈ (UI ������Ʈ)
    //Canvas
    public GameObject UICanvas;
    //HPPanel
    public GameObject HPPanel;
    public GameObject Portrait;
    public GameObject HPContainer;
    //MenuPanel
    public GameObject MenuPanel;
    //public GameObject MenuSelectPanel;
    public GameObject ContinueButton;
    public GameObject SettingButton;
    public GameObject SaveAndExitButton;



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

        if (HPPanel == null) HPPanel = transform.Find("UICanvas/HPPanel")?.gameObject;
        if (Portrait == null) Portrait = transform.Find("UICanvas/HPPanel/Portrait")?.gameObject;
        if (HPContainer == null) HPContainer = transform.Find("UICanvas/HPPanel/HPContainer")?.gameObject;

        if (MenuPanel == null) MenuPanel = transform.Find("UICanvas/MenuPanel")?.gameObject;
        //if (MenuSelectPanel == null) MenuSelectPanel = transform.Find("UICanvas/MenuPanel/MenuSelectPanel")?.gameObject;
        if (ContinueButton == null) ContinueButton = transform.Find("UICanvas/MenuPanel/ContinueButton")?.gameObject;
        if (SettingButton == null) SettingButton = transform.Find("UICanvas/MenuPanel/SettingButton")?.gameObject;
        if (SaveAndExitButton == null) SaveAndExitButton = transform.Find("UICanvas/MenuPanel/SaveAndExitButton")?.gameObject;


    }
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ContinueButton);

    }


}
