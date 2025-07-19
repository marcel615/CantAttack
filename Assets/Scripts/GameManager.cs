using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Manager들 및 자주 참조하는 오브젝트들 연결
    public UIManager UIManager;
    public Player Player;
    public CameraManager CameraManager;



    private void Awake()
    {
        //싱글턴 패턴 생성
        // 기존 GameManager 인스턴스가 존재할 때 && 지금 새로운 GameManager 인스턴스가 생성되려고 할 때
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //중복되지 않도록 지금 새롭게 생성되는 놈은 파괴시킨다
            return;
        }
        // GameManager 인스턴스 처음 할당할 때
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    void Start()
    {
        //인스펙터에서 연결 까먹었을 경우에 대비
        if (UIManager == null) UIManager = transform.Find("UIManager")?.GetComponent<UIManager>();
        if (Player == null) Player = GameObject.Find("Player")?.GetComponent<Player>();
        if (CameraManager == null) CameraManager = GameObject.Find("CameraManager")?.GetComponent<CameraManager>();

    }


}
