using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private Fade fade;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (fade == null) fade = transform.Find("UICanvas/FadePanel")?.GetComponent<Fade>();
    }

    //이벤트 구독
    private void OnEnable()
    {
        //FadeOpen 이벤트 구독
        FadeEvents.OnFadeOpen += FadeOpen;
        //FadeClose 이벤트 구독
        FadeEvents.OnFadeClose += FadeClose;
    }
    private void OnDisable()
    {
        //FadeOpen 이벤트 구독
        FadeEvents.OnFadeOpen -= FadeOpen;
        //FadeClose 이벤트 구독
        FadeEvents.OnFadeClose -= FadeClose;
    }

    //FadeOpen 이벤트 구독
    void FadeOpen(float fadeTime, FadeDirection fadeDirection)
    {
        fade.FadeOpen(fadeTime, fadeDirection);
    }
    //FadeClose 이벤트 구독
    void FadeClose()
    {
        fade.FadeClose();
    }

}
