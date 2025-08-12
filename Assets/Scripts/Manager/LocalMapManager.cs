using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocalMapManager : MonoBehaviour
{
    //카메라 관련
    public CinemachineVirtualCamera CineCamera;
    [SerializeField] private PolygonCollider2D CameraBounds;

    //맵 데이터
    public MapDataSO mapDataSO;

    //포탈 관련
    public List<PortalDataSO> portalList;
    public Dictionary<string, PortalDataSO> portalDict;

    //GameEvent 관련



    private void Awake()
    {
        MapEvents.InvokeLocalMapManagerInit(this);
        //포탈 Dic 초기화
        portalDict = new Dictionary<string, PortalDataSO>();
        SetPortalDic();
    }
    //이벤트 구독
    private void OnEnable()
    {
        //보스전 시작 게임이벤트
        GameEvents.OnBossFightStart += OnBossFightStart;
    }
    private void OnDisable()
    {
        //보스전 시작 게임이벤트
        GameEvents.OnBossFightStart -= OnBossFightStart;
    }
    void OnBossFightStart(string eventID)
    {
        //들어온 EventID가 여기서 가지고 있는 Event들 중에 있을 때
        if (mapDataSO.gameEvents.Any(e => e.gameEventID == eventID))
        {
            Debug.Log("BossFight Start!");
        }
    }

    //포탈 관련
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
