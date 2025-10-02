using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneTransitionEvents
{
    //메인메뉴 Continue 버튼에서 게임 씬으로 씬 전환 요청하는 경우 이벤트
    public static event Action OnContinueToGameScene;
    //메인메뉴 세이브 슬롯에서 게임 씬으로 씬 전환 요청하는 경우 이벤트
    public static event Action<int> OnSaveSlotToGameScene;
    //포탈에서 포탈로 씬 전환 요청하는 경우 이벤트
    public static event Action<string> OnPortalToPortal;
    //Dead 상태에서 Respawn 상태로 씬 전환 요청하는 경우 이벤트
    public static event Action OnDeadToRespawn;
    //SystemMenu 패널에서 메인메뉴로 씬 전환 요청하는 경우 이벤트
    public static event Action<string> OnSystemMenuToMainMenu;


    //메인메뉴 Continue 버튼에서 게임 씬으로 씬 전환 요청하는 경우 이벤트
    public static void InvokeContinueToGameScene()
    {
        OnContinueToGameScene?.Invoke();
    }
    //메인메뉴 세이브 슬롯에서 게임 씬으로 씬 전환 요청하는 경우 이벤트
    public static void InvokeSaveSlotToGameScene(int slotNum)
    {
        OnSaveSlotToGameScene?.Invoke(slotNum);
    }
    //포탈에서 포탈로 씬 전환 요청하는 경우 이벤트
    public static void InvokePortalToPortal(string targetScene)
    {
        OnPortalToPortal?.Invoke(targetScene);
    }
    //Dead 상태에서 Respawn 상태로 씬 전환 요청하는 경우 이벤트
    public static void InvokeDeadToRespawn()
    {
        OnDeadToRespawn?.Invoke();
    }
    //SystemMenu 패널에서 메인메뉴로 씬 전환 요청하는 경우 이벤트
    public static void InvokeSystemMenuToMainMenu(string targetScene)
    {
        OnSystemMenuToMainMenu?.Invoke(targetScene);
    }

}
