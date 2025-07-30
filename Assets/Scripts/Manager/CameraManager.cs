using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class CameraManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    private static CameraManager Instance;

    //Don't Destroy ������Ʈ
    Player player;
    MapManager mapManager;
    CinemachineVirtualCamera cineCamera;


    private void Awake()
    {
        // ���� �ν��Ͻ��� ������ �� && ���� ���ο� �ν��Ͻ��� �����Ƿ��� �� ��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //�ߺ����� �ʵ��� ���� ���Ӱ� �����Ǵ� ���� �ı���Ų��
            return;
        }
        // �ν��Ͻ� ó�� �Ҵ�
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    //�̺�Ʈ ����
    private void OnEnable()
    {
        PlayerEvents.OnPlayerInstance += GetPlayerInstance;
        MapEvents.OnMapManagerInstance += GetMapManagerInstance;
        MapEvents.OnLocalMapManagerInit += GetLocalMapManagerCamera;
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private void OnDisable()
    {
        PlayerEvents.OnPlayerInstance -= GetPlayerInstance;
        MapEvents.OnMapManagerInstance -= GetMapManagerInstance;
        MapEvents.OnLocalMapManagerInit -= GetLocalMapManagerCamera;
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }
    void GetPlayerInstance(Player p)
    {
        player = p;
    }
    void GetMapManagerInstance(MapManager m)
    {
        mapManager = m;
    }
    void GetLocalMapManagerCamera(LocalMapManager local)
    {
        cineCamera = local.CineCamera;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(cineCamera != null)
        {
            cineCamera.Follow = player.transform;
        }
    }

}
