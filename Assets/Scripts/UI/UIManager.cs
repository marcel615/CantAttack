using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    private static UIManager Instance;

    //자식 오브젝트 (UI 오브젝트)
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
