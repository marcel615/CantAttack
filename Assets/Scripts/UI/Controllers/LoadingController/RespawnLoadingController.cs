using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnLoadingController : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private RespawnLoading respawnLoading;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (respawnLoading == null) respawnLoading = transform.Find("LoadingPanel/RespawnLoadingPanel")?.GetComponent<RespawnLoading>();
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //RespawnLoading UI�� Open�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnRespawnLoadingOpen += RespawnLoadingOpen;
        //RespawnLoading UI�� Close�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnRespawnLoadingClose += RespawnLoadingClose;
    }
    private void OnDisable()
    {
        //RespawnLoading UI�� Open�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnRespawnLoadingOpen -= RespawnLoadingOpen;
        //RespawnLoading UI�� Close�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnRespawnLoadingClose -= RespawnLoadingClose;
    }

    //RespawnLoading UI�� Open�ϱ� ���� �̺�Ʈ
    void RespawnLoadingOpen(float fadeTime, string targetScene)
    {
        respawnLoading.RespawnLoadingOpen(fadeTime, targetScene);
    }
    //RespawnLoading UI�� Close�ϱ� ���� �̺�Ʈ
    void RespawnLoadingClose()
    {
        respawnLoading.RespawnLoadingClose();
    }
}
