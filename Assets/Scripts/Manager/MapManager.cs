using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static MapManager Instance;

    //각 씬마다의 LocalMapManager
    public LocalMapManager localMapManager;

    //포탈로 씬 이동 시 관련 변수
    string enterPortalID;
    string targetScene;
    string targetPortalID;
    Vector2 targetPortalPos;
    bool isPortalSceneChange;




    private void Awake()
    {
        // 기존 인스턴스가 존재할 때 && 지금 새로운 인스턴스가 생성되려고 할 때
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //중복되지 않도록 지금 새롭게 생성되는 놈은 파괴시킨다
            return;
        }
        // 인스턴스 처음 할당
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //이벤트 구독
    private void OnEnable()
    {
        //LocalMapManager가 Awake()에서 이벤트 발행
        MapEvents.OnLocalMapManagerInit += GetLocalMapManager;
        //Portal에서 Portal 진입 시 이벤트 발행
        PortalEvents.OnPortalEnter += EnterPortal;
        //PlayerPortal에서 맵 이동하라고 이벤트 발행
        MapEvents.OnRequestMapMove_Portal += MoveScene_Portal;
        //씬 로드 시 이벤트
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        //LocalMapManager가 Awake()에서 이벤트 발행
        MapEvents.OnLocalMapManagerInit -= GetLocalMapManager;
        //Portal에서 Portal 진입 시 이벤트 발행
        PortalEvents.OnPortalEnter -= EnterPortal;
        //PlayerPortal에서 맵 이동하라고 이벤트 발행
        MapEvents.OnRequestMapMove_Portal -= MoveScene_Portal;
        //씬 로드 시 이벤트
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void GetLocalMapManager(LocalMapManager local)
    {
        localMapManager = local;        
    }

    void EnterPortal(string enterP, string targetS, string targetP)
    {
        enterPortalID = enterP;
        targetScene = targetS;
        targetPortalID = targetP;
    }
    void MoveScene_Portal()
    {
        isPortalSceneChange = true;
        //Map 바꾸겠다고 이벤트 발행
        MapEvents.InvokeStartChangeScene();

        //페이드 진행 및 로딩 씬 진행 후 타겟 씬 로드
        FadeEvents.InvokeFadeOpen(targetScene, FadeDirection.FadeOut);
        //로딩씬을 통해서 타겟 씬 로드
        //LoadingSceneLoader.LoadScene(targetScene);
        //SceneManager.LoadScene(targetScene);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(isPortalSceneChange && scene.name != "LoadingScene")
        {
            targetPortalPos = localMapManager.GetPortalPos(targetPortalID);
            MapEvents.InvokeGetPlayerPos(targetPortalPos);
            isPortalSceneChange = false;
        }
    }
    public void Init()
    {
        MapEvents.InvokeMapManagerInstance(this);
    }
}
