using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMapManager : MonoBehaviour
{
    //ī�޶� ����
    public CinemachineVirtualCamera CineCamera;
    [SerializeField] private PolygonCollider2D CameraBounds;

    //��Ż ����
    public List<PortalDataSO> portalList;
    public Dictionary<string, PortalDataSO> portalDict;

    private void Awake()
    {
        MapEvents.InvokeLocalMapManagerInit(this);
        //��Ż Dic �ʱ�ȭ
        portalDict = new Dictionary<string, PortalDataSO>();
        SetPortalDic();
    }

    void SetPortalDic()
    {
        foreach (PortalDataSO portal in portalList)
        {
            if (!portalDict.ContainsKey(portal.portalID))
            {
                portalDict.Add(portal.portalID, portal);
            }
            else
            {
                Debug.LogWarning($"�ߺ��� PortalID �߰�: {portal.portalID} - ���õ�");
            }
        }
    }
    public Vector2 GetPortalPos(string portalID)
    {
        return portalDict[portalID].position;
    }



}
