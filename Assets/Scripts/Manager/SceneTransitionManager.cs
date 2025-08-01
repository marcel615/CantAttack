using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static SceneTransitionManager Instance;

    public float fadeTime = 1f;

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
    private void OnEnable()
    {
        //메인메뉴 세이브 슬롯에서 게임 씬으로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnSaveSlotToGameScene += SaveSlotToGameScene;
        //포탈에서 포탈로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnPortalToPortal += PortalToPortal;
    }

    private void OnDisable()
    {
        //메인메뉴 세이브 슬롯에서 게임 씬으로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnSaveSlotToGameScene -= SaveSlotToGameScene;
        //포탈에서 포탈로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnPortalToPortal -= PortalToPortal;
    }
    void SaveSlotToGameScene(string targetScene)
    {

    }
    void PortalToPortal(string targetScene)
    {
        StartCoroutine(FadeOutToLoadingScene(targetScene, SceneChangeType.Portal));
    }
    IEnumerator FadeOutToLoadingScene(string targetScene, SceneChangeType sceneChangeType)
    {
        //페이드아웃 진행
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //로딩씬 진입
        LoadingSceneLoader.Init(fadeTime, targetScene, sceneChangeType);

    }



}
