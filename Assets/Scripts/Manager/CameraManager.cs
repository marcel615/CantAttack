using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class CameraManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    private static CameraManager Instance;

    //Player
    Player player;


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
        PlayerEvents.OnPlayerInstance += GetPlayerInstance;
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private void OnDisable()
    {
        PlayerEvents.OnPlayerInstance -= GetPlayerInstance;
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }
    void GetPlayerInstance(Player p)
    {
        player = p;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬에 있는 Virtual Camera 찾아서 연결
        CinemachineVirtualCamera currentVCam = FindObjectOfType<CinemachineVirtualCamera>();
        if (currentVCam != null && player != null)
        {
            currentVCam.Follow = player.transform;
        }
    }

}
