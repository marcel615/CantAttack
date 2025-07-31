using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneFadeInputController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private LoadingSceneFade fade;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (fade == null) fade = transform.Find("LoadingUICanvas/FadePanel")?.GetComponent<LoadingSceneFade>();
    }

    //이벤트 구독
    private void OnEnable()
    {
        //LoadingScene의 FadeOpen 이벤트 구독
        FadeEvents.OnLoadingSceneFadeOpen += FadeOpen;
        //LoadingScene의 FadeClose 이벤트 구독
        FadeEvents.OnLoadingSceneFadeClose += FadeClose;
    }
    private void OnDisable()
    {
        //LoadingScene의 FadeOpen 이벤트 구독
        FadeEvents.OnLoadingSceneFadeOpen -= FadeOpen;
        //LoadingScene의 FadeClose 이벤트 구독
        FadeEvents.OnLoadingSceneFadeClose -= FadeClose;
    }

    //LoadingSceneFadeOpen 이벤트 구독
    void FadeOpen(float fadeTime, FadeDirection fadeDirection)
    {
        fade.FadeOpen(fadeTime, fadeDirection);
    }
    //LoadingSceneFadeOpen 이벤트 구독
    void FadeClose()
    {
        fade.FadeClose();
    }
}
