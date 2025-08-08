using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static MapManager Instance;

    //세이브, 로드 변수
    public string saveScene;

    //각 씬마다의 LocalMapManager
    public LocalMapManager localMapManager;

    //포탈로 씬 이동 시 관련 변수
    string enterPortalID;
    string targetScene;
    string targetPortalID;
    PortalWalkDirection walkDirection;
    Vector2 targetPortalPos;
    bool isPortalSceneChange;
    bool isTargetScene;

    //세이브슬롯에서 게임씬으로 이동 시 관련 변수
    bool isSavedSceneLoaded;




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
        //씬 로드 시 이벤트
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        //LocalMapManager가 Awake()에서 이벤트 발행
        MapEvents.OnLocalMapManagerInit -= GetLocalMapManager;
        //Portal에서 Portal 진입 시 이벤트 발행
        PortalEvents.OnPortalEnter -= EnterPortal;
        //씬 로드 시 이벤트
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void GetLocalMapManager(LocalMapManager local)
    {
        localMapManager = local;
        saveScene = localMapManager.mapDataSO.sceneName;
    }

    void EnterPortal(string enterP, string targetS, string targetP, PortalWalkDirection walkDir)
    {
        if (isTargetScene)
        {
            isTargetScene = false;  //플래그 재사용 가능하도록
            return;  //타겟 씬에 도착한 다음의 포탈이면 바로 리턴
        }

        //출발포탈일 때만 실행
        enterPortalID = enterP;
        targetScene = targetS;
        targetPortalID = targetP;
        walkDirection = walkDir;

        isPortalSceneChange = true;
        isTargetScene = false;

        //씬 전환 실시
        SceneTransitionEvents.InvokePortalToPortal(targetScene);
        
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //메인메뉴로 나가면 세이브슬롯 로드할 때 필요한 플래그 false로 초기화
        if(scene.name == "MainMenu")
        {
            isSavedSceneLoaded = false;
        }
        //씬 로드 시 포탈 씬 체인지중이고, 씬 이름이 이전에 저장했던 도착포탈의 이름과 같다면
        if(isPortalSceneChange && scene.name == targetScene)
        {
            if(walkDirection != PortalWalkDirection.Up) //WalkDirection이 Up이었을 경우에는 isTargetScene을 true로 활성화X
                isTargetScene = true;

            targetPortalPos = localMapManager.GetPortalPos(targetPortalID);
            MapEvents.InvokeGetPlayerPos(targetPortalPos);
            isPortalSceneChange = false;
        }
        //세이브슬롯에서부터 로드할 때이고, 씬 이름이 저장된 씬 이름과 같다면
        if (!isSavedSceneLoaded && scene.name == saveScene)
        {
            MapEvents.InvokeSavedSceneLoaded();
            //Context 업데이트
            InputEvents.InvokeContextUpdate(InputContext.Player);
            isSavedSceneLoaded = true;
        }
    }
    public void Init()
    {
        MapEvents.InvokeMapManagerInstance(this);
    }
}
