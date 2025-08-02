using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveExitLoadingController : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private SaveExitLoading saveExitLoading;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (saveExitLoading == null) saveExitLoading = transform.Find("LoadingPanel/SaveExitLoadingPanel")?.GetComponent<SaveExitLoading>();
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //SaveExitLoading UI�� Open�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnSaveExitLoadingOpen += SaveExitLoadingOpen;
        //SaveExitLoading UI�� Close�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnSaveExitLoadingClose += SaveExitLoadingClose;
    }
    private void OnDisable()
    {
        //SaveExitLoading UI�� Open�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnSaveExitLoadingOpen -= SaveExitLoadingOpen;
        //SaveExitLoading UI�� Close�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnSaveExitLoadingClose -= SaveExitLoadingClose;
    }

    //LoadingScene�� SaveSlotLoadingOpen �̺�Ʈ ����
    void SaveExitLoadingOpen(float fadeTime, string targetScene)
    {
        saveExitLoading.SaveExitLoadingOpen(fadeTime, targetScene);
    }
    //LoadingScene�� SaveSlotLoadingClose �̺�Ʈ ����
    void SaveExitLoadingClose()
    {
        saveExitLoading.SaveExitLoadingClose();
    }
}
