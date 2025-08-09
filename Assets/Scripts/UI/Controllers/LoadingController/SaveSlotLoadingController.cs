using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotLoadingController : MonoBehaviour
{
    //자식 오브젝트
    [SerializeField] private SaveSlotLoading saveSlotLoading;

    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (saveSlotLoading == null) saveSlotLoading = transform.Find("LoadingPanel/SaveSlotLoadingPanel")?.GetComponent<SaveSlotLoading>();
    }

    //이벤트 구독
    private void OnEnable()
    {
        //SaveSlotLoading UI를 Open하기 위한 이벤트
        LoadingEvents.OnSaveSlotLoadingOpen += SaveSlotLoadingOpen;
        //SaveSlotLoading UI를 Close하기 위한 이벤트
        LoadingEvents.OnSaveSlotLoadingClose += SaveSlotLoadingClose;
    }
    private void OnDisable()
    {
        //SaveSlotLoading UI를 Open하기 위한 이벤트
        LoadingEvents.OnSaveSlotLoadingOpen -= SaveSlotLoadingOpen;
        //SaveSlotLoading UI를 Close하기 위한 이벤트
        LoadingEvents.OnSaveSlotLoadingClose -= SaveSlotLoadingClose;
    }

    //SaveSlotLoading UI를 Open하기 위한 이벤트
    void SaveSlotLoadingOpen(SceneChangeType sceneChangeType, float fadeTime, string targetScene, int slotNum)
    {
        saveSlotLoading.SaveSlotLoadingOpen(sceneChangeType, fadeTime, targetScene, slotNum);
    }
    //SaveSlotLoading UI를 Close하기 위한 이벤트
    void SaveSlotLoadingClose()
    {
        saveSlotLoading.SaveSlotLoadingClose();
    }
}
