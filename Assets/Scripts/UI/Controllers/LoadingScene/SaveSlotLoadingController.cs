using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotLoadingController : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private SaveSlotLoading saveSlotLoading;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (saveSlotLoading == null) saveSlotLoading = transform.Find("LoadingUICanvas/SaveSlotLoadingPanel")?.GetComponent<SaveSlotLoading>();
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //LoadingScene�� SaveSlotLoadingOpen �̺�Ʈ ����
        LoadingSceneEvents.OnSaveSlotLoadingOpen += SaveSlotLoadingOpen;
        //LoadingScene�� SaveSlotLoadingClose �̺�Ʈ ����
        LoadingSceneEvents.OnSaveSlotLoadingClose += SaveSlotLoadingClose;
    }
    private void OnDisable()
    {
        //LoadingScene�� SaveSlotLoadingOpen �̺�Ʈ ����
        LoadingSceneEvents.OnSaveSlotLoadingOpen -= SaveSlotLoadingOpen;
        //LoadingScene�� SaveSlotLoadingClose �̺�Ʈ ����
        LoadingSceneEvents.OnSaveSlotLoadingClose -= SaveSlotLoadingClose;
    }

    //LoadingScene�� SaveSlotLoadingOpen �̺�Ʈ ����
    void SaveSlotLoadingOpen(SceneChangeType sceneChangeType, float fadeTime, string targetScene, int slotNum)
    {
        saveSlotLoading.SaveSlotLoadingOpen(sceneChangeType, fadeTime, targetScene, slotNum);
    }
    //LoadingScene�� SaveSlotLoadingClose �̺�Ʈ ����
    void SaveSlotLoadingClose()
    {
        saveSlotLoading.SaveSlotLoadingClose();
    }
}
