using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static SceneTransitionManager Instance;

    //페이드 시간
    public float fadeTime = 0.3f;

    //씬 전환 중요 변수들
    SceneChangeType sceneChangeType;
    string targetScene;
    int saveSlotNum;


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
        //메인메뉴 Continue 버튼에서 게임 씬으로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnContinueToGameScene += ContinueToGameScene;
        //메인메뉴 세이브 슬롯에서 게임 씬으로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnSaveSlotToGameScene += SaveSlotToGameScene;
        //포탈에서 포탈로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnPortalToPortal += PortalToPortal;
        //Dead 상태에서 Respawn 상태로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnDeadToRespawn += DeadToRespawn;
        //SystemMenu 패널에서 메인메뉴로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnSystemMenuToMainMenu += SystemMenuToMainMenu;

        //씬 로드 시 이벤트 (로딩씬이 로드되었는지 확인하기 위해)
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnDisable()
    {
        //메인메뉴 Continue 버튼에서 게임 씬으로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnContinueToGameScene -= ContinueToGameScene;
        //메인메뉴 세이브 슬롯에서 게임 씬으로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnSaveSlotToGameScene -= SaveSlotToGameScene;
        //포탈에서 포탈로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnPortalToPortal -= PortalToPortal;
        //Dead 상태에서 Respawn 상태로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnDeadToRespawn -= DeadToRespawn;
        //SystemMenu 패널에서 메인메뉴로 씬 전환 요청하는 경우 이벤트
        SceneTransitionEvents.OnSystemMenuToMainMenu -= SystemMenuToMainMenu;

        //씬 로드 시 이벤트 (로딩씬이 로드되었는지 확인하기 위해)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //LoadingScene 로드 완료 시 이전에 저장한 SceneChangeType에 따라 적절한 패널 오픈 이벤트 발행
        if (scene.name == "LoadingScene")
        {
            switch (sceneChangeType)
            {
                case SceneChangeType.MainMenuContinue:
                    LoadingEvents.InvokeSaveSlotLoadingOpen(sceneChangeType, fadeTime, targetScene, saveSlotNum);

                    break;

                case SceneChangeType.SaveSlot:
                    LoadingEvents.InvokeSaveSlotLoadingOpen(sceneChangeType, fadeTime, targetScene, saveSlotNum);

                    break;

                case SceneChangeType.Portal:
                    LoadingEvents.InvokePortalLoadingOpen(fadeTime, targetScene);

                    break;

                case SceneChangeType.Respawn:
                    LoadingEvents.InvokeRespawnLoadingOpen(fadeTime, targetScene);

                    break;

                case SceneChangeType.SystemMenu:
                    LoadingEvents.InvokeOnSaveExitLoadingOpen(fadeTime, targetScene);

                    break;
            }
        }
        else
        {
            if(scene.name != "BootScene")
            {
                //타겟 씬에 도착했을 때 페이드인 작업 후 Context 정상으로 돌림
                StartCoroutine(TargetSceneFadeIn());
            }
        }
    }

    void ContinueToGameScene()
    {
        //Context 변경 이벤트
        InputEvents.InvokeContextUpdate(InputContext.SceneChange);

        //씬 전환 중요 변수 저장
        sceneChangeType = SceneChangeType.MainMenuContinue;
        targetScene = null;
        saveSlotNum = -1;

        //Fade 코루틴 시작
        StartCoroutine(ContinueFadeOut(sceneChangeType, targetScene, saveSlotNum));
    }
    void SaveSlotToGameScene(int num)
    {
        //Context 변경 이벤트
        InputEvents.InvokeContextUpdate(InputContext.SceneChange);

        //씬 전환 중요 변수 저장
        sceneChangeType = SceneChangeType.SaveSlot;
        targetScene = null;
        saveSlotNum = num;

        //Fade 코루틴 시작
        StartCoroutine(SaveSlotFadeOut(sceneChangeType, targetScene, saveSlotNum));
    }
    void PortalToPortal(string targetS)
    {
        //Context 변경 이벤트
        InputEvents.InvokeContextUpdate(InputContext.SceneChange);

        //씬 전환 중요 변수 저장
        sceneChangeType = SceneChangeType.Portal;
        targetScene = targetS;
        saveSlotNum = -1;

        //Fade 코루틴 시작
        StartCoroutine(PortalFadeOut(sceneChangeType, targetScene, saveSlotNum));
    }
    void DeadToRespawn()
    {
        //Context 변경 이벤트
        InputEvents.InvokeContextUpdate(InputContext.SceneChange);

        //씬 전환 중요 변수 저장
        sceneChangeType = SceneChangeType.Respawn;
        targetScene = null;

        //Fade 코루틴 시작
        StartCoroutine(DeadFadeOut());
    }
    void SystemMenuToMainMenu(string targetS)
    {
        //Context 변경 이벤트
        InputEvents.InvokeContextUpdate(InputContext.SceneChange);

        //씬 전환 중요 변수 저장
        sceneChangeType = SceneChangeType.SystemMenu;
        targetScene = targetS;
        saveSlotNum = -1;

        //Fade 코루틴 시작
        StartCoroutine(SystemMenuFadeOut(sceneChangeType, targetScene, saveSlotNum));
    }

    //코루틴들
    IEnumerator ContinueFadeOut(SceneChangeType sceneChangeType, string targetScene, int num)
    {
        //페이드아웃 진행
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //메인메뉴 UI 닫도록 이벤트 발행
        InputEvents.MainMenu.InvokeMainMenuClose(InputContext.SceneChange);

        //로딩씬 진입
        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator SaveSlotFadeOut(SceneChangeType sceneChangeType, string targetScene, int num)
    {
        //페이드아웃 진행
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //세이브슬롯 UI 닫도록 이벤트 발행
        InputEvents.SaveSlot.InvokeSaveSlotClose(InputContext.SceneChange);

        //로딩씬 진입
        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator PortalFadeOut(SceneChangeType sceneChangeType, string targetScene, int num)
    {
        //페이드아웃 진행
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //로딩씬 진입
        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator DeadFadeOut()
    {
        //페이드아웃 진행
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //로딩씬 진입
        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator SystemMenuFadeOut(SceneChangeType sceneChangeType, string targetScene, int num)
    {
        //페이드아웃 진행
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeOut);
        yield return new WaitForSeconds(fadeTime);

        //시스템메뉴 UI 닫도록 이벤트 발행
        InputEvents.SystemMenu.InvokeSystemMenuClose(InputContext.SceneChange);

        //로딩씬 진입
        SceneManager.LoadScene("LoadingScene");
    }


    //타겟 씬에 도착했을 때 페이드인 작업 후 Context 정상으로 돌림
    IEnumerator TargetSceneFadeIn()
    {
        //페이드인 진행
        FadeEvents.InvokeFadeOpen(fadeTime, FadeDirection.FadeIn);
        yield return new WaitForSeconds(fadeTime);
    }



}
