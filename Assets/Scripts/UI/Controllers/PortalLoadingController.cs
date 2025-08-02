using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLoadingController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private PortalLoading portalLoading;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (portalLoading == null) portalLoading = transform.Find("LoadingUICanvas/PortalLoadingPanel")?.GetComponent<PortalLoading>();
    }

    //이벤트 구독
    private void OnEnable()
    {
        //LoadingScene의 PortalLoadingOpen 이벤트 구독
        LoadingEvents.OnPortalLoadingOpen += PortalLoadingOpen;
        //LoadingScene의 PortalLoadingClose 이벤트 구독
        LoadingEvents.OnPortalLoadingClose += PortalLoadingClose;
    }
    private void OnDisable()
    {
        //LoadingScene의 PortalLoadingOpen 이벤트 구독
        LoadingEvents.OnPortalLoadingOpen -= PortalLoadingOpen;
        //LoadingScene의 PortalLoadingClose 이벤트 구독
        LoadingEvents.OnPortalLoadingClose -= PortalLoadingClose;
    }

    //LoadingScene의 PortalLoadingOpen 이벤트
    void PortalLoadingOpen(float fadeTime, string targetScene)
    {
        portalLoading.PortalLoadingOpen(fadeTime, targetScene);
    }
    //LoadingScene의 PortalLoadingClose 이벤트 
    void PortalLoadingClose()
    {
        portalLoading.PortalLoadingClose();
    }
}
