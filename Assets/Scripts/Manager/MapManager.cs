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
    }
    private void OnDisable()
    {
        MapEvents.OnLocalMapManagerInit -= GetLocalMapManager;
    }

    void GetLocalMapManager(LocalMapManager local)
    {
        localMapManager = local;        
    }
    public void Init()
    {
        MapEvents.InvokeMapManagerInstance(this);
    }
}
