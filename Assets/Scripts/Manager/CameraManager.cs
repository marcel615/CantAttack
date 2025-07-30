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

    //Player
    Player player;


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
        // ���� �ִ� Virtual Camera ã�Ƽ� ����
        CinemachineVirtualCamera currentVCam = FindObjectOfType<CinemachineVirtualCamera>();
        if (currentVCam != null && player != null)
        {
            currentVCam.Follow = player.transform;
        }
    }

}
