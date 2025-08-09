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
        if (portalLoading == null) portalLoading = transform.Find("LoadingPanel/PortalLoadingPanel")?.GetComponent<PortalLoading>();
    }

    //이벤트 구독
    private void OnEnable()
    {
        //PortalLoading UI를 Open하기 위한 이벤트
        LoadingEvents.OnPortalLoadingOpen += PortalLoadingOpen;
        //PortalLoading UI를 Close하기 위한 이벤트
        LoadingEvents.OnPortalLoadingClose += PortalLoadingClose;
    }
    private void OnDisable()
    {
        //PortalLoading UI를 Open하기 위한 이벤트
        LoadingEvents.OnPortalLoadingOpen -= PortalLoadingOpen;
        //PortalLoading UI를 Close하기 위한 이벤트
        LoadingEvents.OnPortalLoadingClose -= PortalLoadingClose;
    }

    //PortalLoading UI를 Open하기 위한 이벤트
    void PortalLoadingOpen(float fadeTime, string targetScene)
    {
        portalLoading.PortalLoadingOpen(fadeTime, targetScene);
    }
    //PortalLoading UI를 Close하기 위한 이벤트
    void PortalLoadingClose()
    {
        portalLoading.PortalLoadingClose();
    }
}
