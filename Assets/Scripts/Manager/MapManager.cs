using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    public static MapManager Instance;

    //�� �������� LocalMapManager
    public LocalMapManager localMapManager;

    //��Ż�� �� �̵� �� ���� ����





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
