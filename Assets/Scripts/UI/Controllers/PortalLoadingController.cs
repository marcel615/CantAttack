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
        if (portalLoading == null) portalLoading = transform.Find("LoadingPanel/PortalLoadingPanel")?.GetComponent<PortalLoading>();
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //PortalLoading UI�� Open�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnPortalLoadingOpen += PortalLoadingOpen;
        //PortalLoading UI�� Close�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnPortalLoadingClose += PortalLoadingClose;
    }
    private void OnDisable()
    {
        //PortalLoading UI�� Open�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnPortalLoadingOpen -= PortalLoadingOpen;
        //PortalLoading UI�� Close�ϱ� ���� �̺�Ʈ
        LoadingEvents.OnPortalLoadingClose -= PortalLoadingClose;
    }

    //PortalLoading UI�� Open�ϱ� ���� �̺�Ʈ
    void PortalLoadingOpen(float fadeTime, string targetScene)
    {
        portalLoading.PortalLoadingOpen(fadeTime, targetScene);
    }
    //PortalLoading UI�� Close�ϱ� ���� �̺�Ʈ
    void PortalLoadingClose()
    {
        portalLoading.PortalLoadingClose();
    }
}
