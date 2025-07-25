using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveHandler : MonoBehaviour, ISaveLoadable
{
    //세이브데이터 저장 및 불러오기 대상들
    [SerializeField] Player player;
    [SerializeField] PlayerStatus playerStatus;

    private void Awake()
    {
        //내 컴포넌트 연결
        player = GetComponent<Player>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    //ISaveLoadable 인터페이스 구현
    public string DicKey => "PlayerSaveHandler";

    public object Save()
    {
        return new PlayerSaveData
        {
            MaxHP = playerStatus.MaxHP,
            CurrentHP = playerStatus.CurrentHP,
            position = player.transform.position,

        };
    }
    public void Load(object saveData)
    {
        PlayerSaveData playerSaveData = saveData as PlayerSaveData;
        if (playerSaveData != null)
        {
            playerStatus.MaxHP = playerSaveData.MaxHP;
            playerStatus.CurrentHP = playerSaveData.CurrentHP;
            player.savePosition = playerSaveData.position;
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
