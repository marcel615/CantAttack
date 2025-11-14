using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveHandler : MonoBehaviour, ISaveLoadable
{
    //세이브데이터 저장 및 불러오기 대상들
    PlayerController playerController;
    PlayerShieldSlotHandler playerShieldSlotHandler;
    PlayerParryModeHandler playerParryModeHandler;

    private void Awake()
    {
        //내 컴포넌트 연결
        playerController = GetComponent<PlayerController>();
        playerShieldSlotHandler = GetComponent<PlayerShieldSlotHandler>();
        playerParryModeHandler = GetComponent<PlayerParryModeHandler>();
    }

    //ISaveLoadable 인터페이스 구현
    public string DicKey => "PlayerSaveHandler";

    public object Save()
    {
        return new PlayerSaveData
        {
            MaxHP = playerController.MaxHP,
            CurrentHP = playerController.CurrentHP,
            position = playerController.savePosition,
            isDoubleJumpUnlocked = playerController.isDoubleJumpUnlocked,
            currentShieldSlotsType = playerShieldSlotHandler.currentShieldSlotsType,
            currentShieldSlotIndex = playerShieldSlotHandler.currentIndex,
            currentParryModeSlotsType = playerParryModeHandler.currentParryModeSlotsType,
            currentParryModeSlotIndex = playerParryModeHandler.currentIndex,
        };
    }
    public void Load(object saveData)
    {
        PlayerSaveData playerSaveData = saveData as PlayerSaveData;
        if (playerSaveData != null)
        {
            playerController.MaxHP = playerSaveData.MaxHP;
            playerController.CurrentHP = playerSaveData.CurrentHP;
            playerController.savePosition = playerSaveData.position;
            playerController.isDoubleJumpUnlocked = playerSaveData.isDoubleJumpUnlocked;
            playerShieldSlotHandler.currentShieldSlotsType = playerSaveData.currentShieldSlotsType;
            playerShieldSlotHandler.currentIndex = playerSaveData.currentShieldSlotIndex;
            playerParryModeHandler.currentParryModeSlotsType = playerSaveData.currentParryModeSlotsType;
            playerParryModeHandler.currentIndex = playerSaveData.currentParryModeSlotIndex;
        }
    }

    //이벤트 구독
    private void OnEnable()
    {
        SystemEvents.OnSaveDicKeyRequest += SaveDicKey;
    }
    private void OnDisable()
    {
        SystemEvents.OnSaveDicKeyRequest -= SaveDicKey;
    }
    //SaveManager에서 딕셔너리 구성하는 과정
    void SaveDicKey(SaveManager saveManager)
    {
        saveManager.GetDicKey(this);
    }

}
