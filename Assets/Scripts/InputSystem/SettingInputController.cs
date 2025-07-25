using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingInputController : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private Setting setting;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (setting == null) setting = transform.Find("UICanvas/SettingPanel")?.GetComponent<Setting>();

    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //SettingOpen �̺�Ʈ ����
        InputEvents.Setting.OnSettingOpen += SettingOpen;

        //Cancel �̺�Ʈ ����
        InputEvents.Setting.OnCancel += ESC;
        //Submit �̺�Ʈ ����
        InputEvents.Setting.OnSubmit += Enter;
        //Interact �̺�Ʈ ����
        InputEvents.Setting.OnInteract += E;
    }
    private void OnDisable()
    {
        //SettingOpen �̺�Ʈ ����
        InputEvents.Setting.OnSettingOpen -= SettingOpen;

        //�̵� �̺�Ʈ ����
        InputEvents.Setting.OnCancel -= ESC;
        //Submit �̺�Ʈ ����
        InputEvents.Setting.OnSubmit -= Enter;
        //Interact �̺�Ʈ ����
        InputEvents.Setting.OnInteract -= E;
    }

    //SettingOpen �̺�Ʈ ����
    void SettingOpen()
    {
        setting.SettingOpen();
    }


    //Cancel �̺�Ʈ ����
    void ESC(bool esc)
    {
        if (esc)
            setting.ESC(esc);
    }
    //Submit �̺�Ʈ ����
    void Enter(bool enter)
    {
        if (enter)
            setting.Enter(enter);
    }
    //Interact �̺�Ʈ ����
    void E(bool e)
    {
        if (e)
            setting.E(e);
    }
}
