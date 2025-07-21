using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Manager�� �� ���� �����ϴ� ������Ʈ�� ����
    public UIManager UIManager;
    public Player Player;
    public CameraManager CameraManager;



    private void Awake()
    {
        //�̱��� ���� ����
        // ���� GameManager �ν��Ͻ��� ������ �� && ���� ���ο� GameManager �ν��Ͻ��� �����Ƿ��� �� ��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //�ߺ����� �ʵ��� ���� ���Ӱ� �����Ǵ� ���� �ı���Ų��
            return;
        }
        // GameManager �ν��Ͻ� ó�� �Ҵ��� ��
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    void Start()
    {
        //�ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (UIManager == null) UIManager = transform.Find("UIManager")?.GetComponent<UIManager>();
        if (Player == null) Player = GameObject.Find("Player")?.GetComponent<Player>();
        if (CameraManager == null) CameraManager = GameObject.Find("CameraManager")?.GetComponent<CameraManager>();

    }


}
