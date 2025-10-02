using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnLoadingController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private RespawnLoading respawnLoading;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (respawnLoading == null) respawnLoading = transform.Find("LoadingPanel/RespawnLoadingPanel")?.GetComponent<RespawnLoading>();
    }

    //이벤트 구독
    private void OnEnable()
    {
        //RespawnLoading UI를 Open하기 위한 이벤트
        LoadingEvents.OnRespawnLoadingOpen += RespawnLoadingOpen;
        //RespawnLoading UI를 Close하기 위한 이벤트
        LoadingEvents.OnRespawnLoadingClose += RespawnLoadingClose;
    }
    private void OnDisable()
    {
        //RespawnLoading UI를 Open하기 위한 이벤트
        LoadingEvents.OnRespawnLoadingOpen -= RespawnLoadingOpen;
        //RespawnLoading UI를 Close하기 위한 이벤트
        LoadingEvents.OnRespawnLoadingClose -= RespawnLoadingClose;
    }

    //RespawnLoading UI를 Open하기 위한 이벤트
    void RespawnLoadingOpen(float fadeTime, string targetScene)
    {
        respawnLoading.RespawnLoadingOpen(fadeTime, targetScene);
    }
    //RespawnLoading UI를 Close하기 위한 이벤트
    void RespawnLoadingClose()
    {
        respawnLoading.RespawnLoadingClose();
    }
}
