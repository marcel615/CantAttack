using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveExitLoadingController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private SaveExitLoading saveExitLoading;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (saveExitLoading == null) saveExitLoading = transform.Find("LoadingPanel/SaveExitLoadingPanel")?.GetComponent<SaveExitLoading>();
    }

    //이벤트 구독
    private void OnEnable()
    {
        //SaveExitLoading UI를 Open하기 위한 이벤트
        LoadingEvents.OnSaveExitLoadingOpen += SaveExitLoadingOpen;
        //SaveExitLoading UI를 Close하기 위한 이벤트
        LoadingEvents.OnSaveExitLoadingClose += SaveExitLoadingClose;
    }
    private void OnDisable()
    {
        //SaveExitLoading UI를 Open하기 위한 이벤트
        LoadingEvents.OnSaveExitLoadingOpen -= SaveExitLoadingOpen;
        //SaveExitLoading UI를 Close하기 위한 이벤트
        LoadingEvents.OnSaveExitLoadingClose -= SaveExitLoadingClose;
    }

    //LoadingScene의 SaveSlotLoadingOpen 이벤트 구독
    void SaveExitLoadingOpen(float fadeTime, string targetScene)
    {
        saveExitLoading.SaveExitLoadingOpen(fadeTime, targetScene);
    }
    //LoadingScene의 SaveSlotLoadingClose 이벤트 구독
    void SaveExitLoadingClose()
    {
        saveExitLoading.SaveExitLoadingClose();
    }
}
