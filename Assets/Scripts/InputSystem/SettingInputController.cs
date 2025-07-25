using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingInputController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private Setting setting;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (setting == null) setting = transform.Find("UICanvas/SettingPanel")?.GetComponent<Setting>();

    }

    //이벤트 구독
    private void OnEnable()
    {
        //SettingOpen 이벤트 구독
        InputEvents.Setting.OnSettingOpen += SettingOpen;

        //Cancel 이벤트 구독
        InputEvents.Setting.OnCancel += ESC;
        //Submit 이벤트 구독
        InputEvents.Setting.OnSubmit += Enter;
        //Interact 이벤트 구독
        InputEvents.Setting.OnInteract += E;
    }
    private void OnDisable()
    {
        //SettingOpen 이벤트 구독
        InputEvents.Setting.OnSettingOpen -= SettingOpen;

        //이동 이벤트 구독
        InputEvents.Setting.OnCancel -= ESC;
        //Submit 이벤트 구독
        InputEvents.Setting.OnSubmit -= Enter;
        //Interact 이벤트 구독
        InputEvents.Setting.OnInteract -= E;
    }

    //SettingOpen 이벤트 구독
    void SettingOpen()
    {
        setting.SettingOpen();
    }


    //Cancel 이벤트 구독
    void ESC(bool esc)
    {
        if (esc)
            setting.ESC(esc);
    }
    //Submit 이벤트 구독
    void Enter(bool enter)
    {
        if (enter)
            setting.Enter(enter);
    }
    //Interact 이벤트 구독
    void E(bool e)
    {
        if (e)
            setting.E(e);
    }
}
