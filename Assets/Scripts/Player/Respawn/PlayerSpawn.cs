using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    Player player;
    Animator animator;

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //세이브슬롯에서 게임씬으로 로드가 완료되었을 때
        MapEvents.OnSavedSceneLoaded += OnSavedSceneLoaded;
        //플레이어가 리스폰해서 게임씬으로 로드가 완료되었을 때
        MapEvents.OnRespawnSceneLoaded += OnRespawnSceneLoaded;
    }
    private void OnDisable()
    {
        //세이브슬롯에서 게임씬으로 로드가 완료되었을 때
        MapEvents.OnSavedSceneLoaded -= OnSavedSceneLoaded;
        //플레이어가 리스폰해서 게임씬으로 로드가 완료되었을 때
        MapEvents.OnRespawnSceneLoaded -= OnRespawnSceneLoaded;
    }
    //세이브 로드 및 리스폰 이후 초기화
    void OnSavedSceneLoaded()
    {
        //플레이어 위치 초기화
        transform.position = player.savePosition;

        //플레이어 스폰 이벤트 발행
        PlayerEvents.InvokePlayerSpawned_HPUIManager(player.status.MaxHP, player.status.CurrentHP);

        //Context 업데이트
        InputEvents.InvokeContextUpdate(InputContext.Player);
    }
    //플레이어가 리스폰해서 게임씬으로 로드가 완료되었을 때
    void OnRespawnSceneLoaded()
    {
        //플레이어 위치 초기화
        transform.position = player.savePosition;

        //체력 풀로 채우고
        player.status.CurrentHP = player.status.MaxHP;
        //히트박스 다시 키고
        player.playerHitBoxCollider.enabled = true;
        //애니메이션 설정하고
        animator.SetBool("isDead", false);

        //플레이어 스폰 이벤트 발행
        PlayerEvents.InvokePlayerSpawned_HPUIManager(player.status.MaxHP, player.status.CurrentHP);

        //Context 업데이트
        InputEvents.InvokeContextUpdate(InputContext.Player);

    }

}
