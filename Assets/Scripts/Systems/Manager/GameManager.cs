using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static GameManager Instance { get; private set; }

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
    //이벤트 구독
    private void OnEnable()
    {
        //GameManager한테 TimeScale 바꿔달라고 요청하는 이벤트
        SystemEvents.OnChangeTimeScale += ChangeTimeScale;
    }
    private void OnDisable()
    {
        //GameManager한테 TimeScale 바꿔달라고 요청하는 이벤트
        SystemEvents.OnChangeTimeScale += ChangeTimeScale;
    }

    void ChangeTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }


}
