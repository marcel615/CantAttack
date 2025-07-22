using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static GameManager Instance { get; private set; }

    //Manager들 및 자주 참조하는 오브젝트들 연결
    /*
    public UIManager UIManager;
    public Player Player;
    public CameraManager CameraManager;
    */

    //지금 실행이 테스트인지 체크하는 플래그
    public bool isTest;


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

    void Start()
    {
        /*
        //인스펙터에서 연결 까먹었을 경우에 대비
        if (UIManager == null) UIManager = transform.Find("UIManager")?.GetComponent<UIManager>();
        if (Player == null) Player = GameObject.Find("Player")?.GetComponent<Player>();
        if (CameraManager == null) CameraManager = GameObject.Find("CameraManager")?.GetComponent<CameraManager>();
        */
    }


}
