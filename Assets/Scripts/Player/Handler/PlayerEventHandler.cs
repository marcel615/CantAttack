using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventHandler : MonoBehaviour
{
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    //이벤트 구독
    private void OnEnable()
    {
        //PlayerHitBox에서 Hit되었을 때
        PlayerEvents.OnPlayerHitBoxHitted_PlayerDamageHandler += playerController.OnDamaged;
        //Player가 사망했을 때
        PlayerEvents.OnPlayerDead += playerController.OnPlayerDead;

        //세이브슬롯에서 게임씬으로 로드가 완료되었을 때
        MapEvents.OnSavedSceneLoaded += playerController.OnSavedSceneLoaded;
        //플레이어가 리스폰해서 게임씬으로 로드가 완료되었을 때
        MapEvents.OnRespawnSceneLoaded += playerController.OnRespawnSceneLoaded;

        //Portal 진입 시 이벤트 
        PortalEvents.OnPortalEnter += playerController.OnPortalEnter;

        //SavePoint에서 저장하기 직전에 보내는 이벤트
        SystemEvents.OnSavePointNotice += playerController.SavePlayerPos;
    }
    private void OnDisable()
    {
        //PlayerHitBox에서 Hit되었을 때
        PlayerEvents.OnPlayerHitBoxHitted_PlayerDamageHandler -= playerController.OnDamaged;
        //Player가 사망했을 때
        PlayerEvents.OnPlayerDead -= playerController.OnPlayerDead;

        //세이브슬롯에서 게임씬으로 로드가 완료되었을 때
        MapEvents.OnSavedSceneLoaded -= playerController.OnSavedSceneLoaded;
        //플레이어가 리스폰해서 게임씬으로 로드가 완료되었을 때
        MapEvents.OnRespawnSceneLoaded -= playerController.OnRespawnSceneLoaded;

        //Portal 진입 시 이벤트 
        PortalEvents.OnPortalEnter -= playerController.OnPortalEnter;

        //SavePoint에서 저장하기 직전에 보내는 이벤트
        SystemEvents.OnSavePointNotice -= playerController.SavePlayerPos;
    }



}
