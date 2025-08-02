using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLoadingController : MonoBehaviour
{
    //�ڽ� ������Ʈ
    [SerializeField] private PortalLoading portalLoading;

    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (portalLoading == null) portalLoading = transform.Find("LoadingUICanvas/PortalLoadingPanel")?.GetComponent<PortalLoading>();
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //LoadingScene�� PortalLoadingOpen �̺�Ʈ ����
        LoadingEvents.OnPortalLoadingOpen += PortalLoadingOpen;
        //LoadingScene�� PortalLoadingClose �̺�Ʈ ����
        LoadingEvents.OnPortalLoadingClose += PortalLoadingClose;
    }
    private void OnDisable()
    {
        //LoadingScene�� PortalLoadingOpen �̺�Ʈ ����
        LoadingEvents.OnPortalLoadingOpen -= PortalLoadingOpen;
        //LoadingScene�� PortalLoadingClose �̺�Ʈ ����
        LoadingEvents.OnPortalLoadingClose -= PortalLoadingClose;
    }

    //LoadingScene�� PortalLoadingOpen �̺�Ʈ
    void PortalLoadingOpen(float fadeTime, string targetScene)
    {
        portalLoading.PortalLoadingOpen(fadeTime, targetScene);
    }
    //LoadingScene�� PortalLoadingClose �̺�Ʈ 
    void PortalLoadingClose()
    {
        portalLoading.PortalLoadingClose();
    }
}
