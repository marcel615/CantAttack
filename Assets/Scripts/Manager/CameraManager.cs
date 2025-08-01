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
        //Player�� ������ ��
        PlayerEvents.OnPlayerInstance += GetPlayerInstance;
        //MapManager�� ������ ��
        MapEvents.OnMapManagerInstance += GetMapManagerInstance;
        //LocalMapManager�� ������ ��
        MapEvents.OnLocalMapManagerInit += GetLocalMapManagerCamera;

        //Camera Follow ������ ��
        CameraEvents.OnCameraFollowReset += ResetCineCamera;
        //�� �ε�� ��
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private void OnDisable()
    {
        //Player�� ������ ��
        PlayerEvents.OnPlayerInstance -= GetPlayerInstance;
        //MapManager�� ������ ��
        MapEvents.OnMapManagerInstance -= GetMapManagerInstance;
        //LocalMapManager�� ������ ��
        MapEvents.OnLocalMapManagerInit -= GetLocalMapManagerCamera;

        //Camera Follow ������ ��
        CameraEvents.OnCameraFollowReset -= ResetCineCamera;
        //�� �ε�� ��
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }
    //Player�� ������ ��
    void GetPlayerInstance(Player p)
    {
        player = p;
    }
    //MapManager�� ������ ��
    void GetMapManagerInstance(MapManager m)
    {
        mapManager = m;
    }
    //LocalMapManager�� ������ ��
    void GetLocalMapManagerCamera(LocalMapManager local)
    {
        cineCamera = local.CineCamera;
    }

    //Camera Follow ������ ��
    void ResetCineCamera()
    {
        if (cineCamera != null)
        {
            cineCamera.Follow = null;
        }
    }
    //�� �ε�� ��
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(cineCamera != null && scene.name != "LoadingScene")
        {
            cineCamera.Follow = player.transform;
        }
    }

}
