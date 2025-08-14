using Cinemachine;
using System;
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
    string currentEventID;
    Transform bossTransform;



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
        //보스전 끝날 때 이벤트
        GameEvents.OnBossFightEnd += OnBossFightEnd;

        //도전 구역(Trial Area) 시작될 때 이벤트
        GameEvents.OnTrialAreaStart += OnTrialAreaStart;
        //도전 구역(Trial Area) 끝날 때 이벤트
        GameEvents.OnTrialAreaEnd += OnTrialAreaEnd;

    }
    private void OnDisable()
    {
        //보스전 시작 게임이벤트
        GameEvents.OnBossFightStart -= OnBossFightStart;
        //보스전 끝날 때 이벤트
        GameEvents.OnBossFightEnd -= OnBossFightEnd;

        //도전 구역(Trial Area) 시작될 때 이벤트
        GameEvents.OnTrialAreaStart -= OnTrialAreaStart;
        //도전 구역(Trial Area) 끝날 때 이벤트
        GameEvents.OnTrialAreaEnd -= OnTrialAreaEnd;
    }
    void OnBossFightStart(string eventID, Transform bossT)
    {
        //들어온 EventID가 여기서 가지고 있는 Event들 중에 있을 때
        if (mapDataSO.gameEvents.Any(e => e.gameEventID == eventID))
        {
            Debug.Log("BossFight Start!");
            currentEventID = eventID;
            bossTransform = bossT;
            StartCoroutine(BossFightStartSequence(1f));
        }
    }
    private IEnumerator BossFightStartSequence(float sequenceTime)
    {
        //플레이어 조작 멈추고(컨텍스트 변경)
        InputEvents.InvokeContextUpdate(InputContext.Sequence);

        //보스방 문 닫고
        GameEvents.InvokeRoomGateActive(currentEventID);

        //카메라 잠시 보스 Follow하도록
        CameraEvents.InvokeCameraFollowChange(bossTransform);

        //연출 시간 후
        yield return new WaitForSeconds(sequenceTime);

        //카메라 다시 플레이어 Follow하도록
        CameraEvents.InvokeCameraFollowPlayer();

        //플레이어 조작 재개(컨텍스트 변경)
        InputEvents.InvokeContextUpdate(InputContext.Player);
    }
    void OnBossFightEnd()
    {
        Debug.Log("BossFight End!");

        StartCoroutine(BossFightEndSequence(2f));

        //해당 이벤트 완료되었다고 추가
        GameEventManager.Instance.AddGameEventCompleted(currentEventID);
    }
    private IEnumerator BossFightEndSequence(float sequenceTime)
    {
        //플레이어 조작 멈추고(컨텍스트 변경)
        InputEvents.InvokeContextUpdate(InputContext.Sequence);

        //카메라 잠시 보스 Follow하도록
        CameraEvents.InvokeCameraFollowChange(bossTransform);

        //슬로우모션 진행
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;

        //연출 시간 후
        yield return new WaitForSeconds(sequenceTime);

        //카메라 다시 플레이어 Follow하도록
        CameraEvents.InvokeCameraFollowPlayer();

        //보스방 문 열고
        GameEvents.InvokeRoomGateDeActive(currentEventID);

        //플레이어 조작 재개(컨텍스트 변경)
        InputEvents.InvokeContextUpdate(InputContext.Player);
    }

    void OnTrialAreaStart(string eventID)
    {
        //들어온 EventID가 여기서 가지고 있는 Event들 중에 있을 때
        if (mapDataSO.gameEvents.Any(e => e.gameEventID == eventID))
        {
            Debug.Log("Trial Start!");
            currentEventID = eventID;

            //도전방 문 닫고
            GameEvents.InvokeRoomGateActive(currentEventID);
        }
    }
    void OnTrialAreaEnd(GameObject rewardObject)
    {
        Debug.Log("Trial End!");

        StartCoroutine(TrialAreaEndSequence(1f, rewardObject));

        //해당 이벤트 완료되었다고 추가
        GameEventManager.Instance.AddGameEventCompleted(currentEventID);
    }
    private IEnumerator TrialAreaEndSequence(float sequenceTime, GameObject rewardObject)
    {
        //도전방 문 열고
        GameEvents.InvokeRoomGateDeActive(currentEventID);

        if (rewardObject != null)
        {
            rewardObject.SetActive(true);

            //플레이어 조작 멈추고(컨텍스트 변경)
            InputEvents.InvokeContextUpdate(InputContext.Sequence);

            //연출 시간 후
            yield return new WaitForSeconds(sequenceTime);

            //플레이어 조작 재개(컨텍스트 변경)
            InputEvents.InvokeContextUpdate(InputContext.Player);

            //보상 아이템의 OnAcquire() 메서드 실행
            rewardObject.GetComponent<ItemBase>().OnAcquire();
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
