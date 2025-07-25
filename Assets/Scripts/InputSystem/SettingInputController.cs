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
        //Cancel �̺�Ʈ ����
        InputEvents.OnCancel += ESC;
        //Submit �̺�Ʈ ����
        InputEvents.OnSubmit += Enter;
        //Interact �̺�Ʈ ����
        InputEvents.OnInteract += E;
    }
    private void OnDisable()
    {
        //�̵� �̺�Ʈ ����
        InputEvents.OnCancel -= ESC;
        //Submit �̺�Ʈ ����
        InputEvents.OnSubmit -= Enter;
        //Interact �̺�Ʈ ����
        InputEvents.OnInteract -= E;
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
