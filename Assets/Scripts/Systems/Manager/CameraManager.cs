using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class CameraManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static CameraManager Instance;

    //Don't Destroy 오브젝트
    PlayerController playerController;
    MapManager mapManager;

    //추적할 씨네카메라
    CinemachineVirtualCamera cineCamera;


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
        //PlayerController가 스폰될 때
        PlayerEvents.OnPlayerControllerInstance += GetPlayerControllerInstance;
        //MapManager가 스폰될 때
        MapEvents.OnMapManagerInstance += GetMapManagerInstance;
        //LocalMapManager가 스폰될 때
        MapEvents.OnLocalMapManagerInit += GetLocalMapManagerCamera;

        //Camera Follow 리셋할 때
        CameraEvents.OnCameraFollowReset += ResetCameraFollow;
        //Camera Follow 변경할 때
        CameraEvents.OnCameraFollowChange += ChangeCameraFollow;
        //Camera Follow 플레이어로 변경할 때
        CameraEvents.OnCameraFollowPlayer += PlayerCameraFollow;
        //CineCamera 교체할 때
        CameraEvents.OnSwitchCamera += SwitchCamera;

        //씬 로드될 때
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        //PlayerController가 스폰될 때
        PlayerEvents.OnPlayerControllerInstance -= GetPlayerControllerInstance;
        //MapManager가 스폰될 때
        MapEvents.OnMapManagerInstance -= GetMapManagerInstance;
        //LocalMapManager가 스폰될 때
        MapEvents.OnLocalMapManagerInit -= GetLocalMapManagerCamera;

        //Camera Follow 리셋할 때
        CameraEvents.OnCameraFollowReset -= ResetCameraFollow;
        //Camera Follow 변경할 때
        CameraEvents.OnCameraFollowChange -= ChangeCameraFollow;
        //Camera Follow 플레이어로 변경할 때
        CameraEvents.OnCameraFollowPlayer -= PlayerCameraFollow;
        //CineCamera 교체할 때
        CameraEvents.OnSwitchCamera -= SwitchCamera;

        //씬 로드될 때
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    //PlayerController가 스폰될 때
    void GetPlayerControllerInstance(PlayerController p)
    {
        playerController = p;
    }
    //MapManager가 스폰될 때
    void GetMapManagerInstance(MapManager m)
    {
        mapManager = m;
    }
    //LocalMapManager가 스폰될 때
    void GetLocalMapManagerCamera(LocalMapManager local)
    {
        cineCamera = local.CineCamera;
    }

    //Camera Follow 리셋할 때
    void ResetCameraFollow()
    {
        if (cineCamera != null)
        {
            cineCamera.Follow = null;
        }
    }
    //Camera Follow 변경할 때
    void ChangeCameraFollow(Transform changeTransform)
    {
        if (cineCamera != null)
        {
            cineCamera.Follow = changeTransform;
        }
    }
    //Camera Follow 플레이어로 변경할 때
    void PlayerCameraFollow()
    {
        if (cineCamera != null)
        {
            cineCamera.Follow = playerController.cameraFollowTransform;
        }
    }
    //씬 로드될 때
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(cineCamera != null && scene.name != "LoadingScene")
        {
            cineCamera.Follow = playerController.cameraFollowTransform;
        }
    }
    void SwitchCamera(CinemachineVirtualCamera newCam)
    {
        cineCamera = newCam;
        cineCamera.Follow = playerController.cameraFollowTransform;
    }

}
