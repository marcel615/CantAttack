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
        if (saveSlotLoading == null) saveSlotLoading = transform.Find("LoadingUICanvas/SaveSlotLoadingPanel")?.GetComponent<SaveSlotLoading>();
    }

    //이벤트 구독
    private void OnEnable()
    {
        //LoadingScene의 SaveSlotLoadingOpen 이벤트 구독
        LoadingSceneEvents.OnSaveSlotLoadingOpen += SaveSlotLoadingOpen;
        //LoadingScene의 SaveSlotLoadingClose 이벤트 구독
        LoadingSceneEvents.OnSaveSlotLoadingClose += SaveSlotLoadingClose;
    }
    private void OnDisable()
    {
        //LoadingScene의 SaveSlotLoadingOpen 이벤트 구독
        LoadingSceneEvents.OnSaveSlotLoadingOpen -= SaveSlotLoadingOpen;
        //LoadingScene의 SaveSlotLoadingClose 이벤트 구독
        LoadingSceneEvents.OnSaveSlotLoadingClose -= SaveSlotLoadingClose;
    }

    //LoadingScene의 SaveSlotLoadingOpen 이벤트 구독
    void SaveSlotLoadingOpen(SceneChangeType sceneChangeType, float fadeTime, string targetScene, int slotNum)
    {
        saveSlotLoading.SaveSlotLoadingOpen(sceneChangeType, fadeTime, targetScene, slotNum);
    }
    //LoadingScene의 SaveSlotLoadingClose 이벤트 구독
    void SaveSlotLoadingClose()
    {
        saveSlotLoading.SaveSlotLoadingClose();
    }
}
