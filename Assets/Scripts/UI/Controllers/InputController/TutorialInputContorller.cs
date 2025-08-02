using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInputContorller : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private Tutorial tutorial;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (tutorial == null) tutorial = transform.Find("UICanvas/TutorialPanel")?.GetComponent<Tutorial>();
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //TutorialOpen �̺�Ʈ ����
        TutorialEvents.OnTutorialOpen += TutorialOpen;
        //TutorialClose �̺�Ʈ ����
        TutorialEvents.OnTutorialClose += TutorialClose;
    }
    private void OnDisable()
    {
        //TutorialOpen �̺�Ʈ ����
        TutorialEvents.OnTutorialOpen -= TutorialOpen;
        //TutorialClose �̺�Ʈ ����
        TutorialEvents.OnTutorialClose -= TutorialClose;
    }

    //TutorialOpen �̺�Ʈ ����
    void TutorialOpen(string message)
    {
        tutorial.TutorialOpen(message);
    }
    //TutorialClose �̺�Ʈ ����
    void TutorialClose()
    {
        tutorial.TutorialClose();
    }
}
