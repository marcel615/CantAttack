using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static MessageManager Instance;

    //내 컴포넌트
    public TutorialMessageManager tutorialMessageManager;

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


        //내 컴포넌트 연결
        tutorialMessageManager = GetComponent<TutorialMessageManager>();


    }

}
