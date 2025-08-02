using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInputContorller : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private Tutorial tutorial;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (tutorial == null) tutorial = transform.Find("UICanvas/TutorialPanel")?.GetComponent<Tutorial>();
    }

    //이벤트 구독
    private void OnEnable()
    {
        //TutorialOpen 이벤트 구독
        TutorialEvents.OnTutorialOpen += TutorialOpen;
        //TutorialClose 이벤트 구독
        TutorialEvents.OnTutorialClose += TutorialClose;
    }
    private void OnDisable()
    {
        //TutorialOpen 이벤트 구독
        TutorialEvents.OnTutorialOpen -= TutorialOpen;
        //TutorialClose 이벤트 구독
        TutorialEvents.OnTutorialClose -= TutorialClose;
    }

    //TutorialOpen 이벤트 구독
    void TutorialOpen(string message)
    {
        tutorial.TutorialOpen(message);
    }
    //TutorialClose 이벤트 구독
    void TutorialClose()
    {
        tutorial.TutorialClose();
    }
}
