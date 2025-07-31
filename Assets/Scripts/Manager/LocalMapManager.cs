using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMapManager : MonoBehaviour
{
    //카메라 관련
    public CinemachineVirtualCamera CineCamera;
    [SerializeField] private PolygonCollider2D CameraBounds;

    //포탈 관련
    public List<PortalDataSO> portalList;
    public Dictionary<string, PortalDataSO> portalDict;

    private void Awake()
    {
        MapEvents.InvokeLocalMapManagerInit(this);
        //포탈 Dic 초기화
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
                Debug.LogWarning($"중복된 PortalID 발견: {portal.portalID} - 무시됨");
            }
        }
    }
    public Vector2 GetPortalPos(string portalID)
    {
        return portalDict[portalID].position;
    }



}
