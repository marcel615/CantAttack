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
        if (saveSlotLoading == null) saveSlotLoading = transform.Find("LoadingPanel/SaveSlotLoadingPanel")?.GetComponent<SaveSlotLoading>();
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //SaveSlotLoading UI�� Open�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnSaveSlotLoadingOpen += SaveSlotLoadingOpen;
        //SaveSlotLoading UI�� Close�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnSaveSlotLoadingClose += SaveSlotLoadingClose;
    }
    private void OnDisable()
    {
        //SaveSlotLoading UI�� Open�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnSaveSlotLoadingOpen -= SaveSlotLoadingOpen;
        //SaveSlotLoading UI�� Close�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnSaveSlotLoadingClose -= SaveSlotLoadingClose;
    }

    //SaveSlotLoading UI�� Open�ϱ� ���� �̺�Ʈ
    void SaveSlotLoadingOpen(SceneChangeType sceneChangeType, float fadeTime, string targetScene, int slotNum)
    {
        saveSlotLoading.SaveSlotLoadingOpen(sceneChangeType, fadeTime, targetScene, slotNum);
    }
    //SaveSlotLoading UI�� Close�ϱ� ���� �̺�Ʈ
    void SaveSlotLoadingClose()
    {
        saveSlotLoading.SaveSlotLoadingClose();
    }
}
