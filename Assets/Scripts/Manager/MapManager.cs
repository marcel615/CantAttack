using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static MapManager Instance;

    public LocalMapManager localMapManager;
    Tilemap tilemap;
    Vector3 minPos;
    Vector3 maxPos;

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
        MapEvents.OnLocalMapManagerInit += GetLocalMapManager;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        MapEvents.OnLocalMapManagerInit -= GetLocalMapManager;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void GetLocalMapManager(LocalMapManager local)
    {
        localMapManager = local;        
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        tilemap = FindObjectOfType<Tilemap>();
        if (tilemap != null)
        {
            SetCameraMinMaxPosition();
            SystemEvents.InvokeGetMapPos(minPos, maxPos);
        }

    }

    //카메라의 Min, Max Position을 획득하기 위한 메소드
    void SetCameraMinMaxPosition()
    {
        minPos = tilemap.localBounds.min;
        minPos.y = minPos.y + 1; //타일맵의 타일앵커가 왼쪽 하단이 아니라 정중앙이기 때문에 발생하는 오차 정정
        maxPos = tilemap.localBounds.max;
        maxPos.x = maxPos.x - 1; //오차 정정

        //경계값에다가 해상도에 맞는 가로, 세로 폭을 더하고 빼는 작업으로 적절한 카메라 위치 조정
        float cameraHeightHalf = Camera.main.orthographicSize;
        float cameraWidthHalf = cameraHeightHalf * Camera.main.aspect;

        minPos.x = minPos.x + cameraWidthHalf;
        minPos.y = minPos.y + cameraHeightHalf;
        maxPos.x = maxPos.x - cameraWidthHalf;
        maxPos.y = maxPos.y - cameraHeightHalf;

    }
    public void Init()
    {
        MapEvents.InvokeMapManagerInstance(this);
    }
}
