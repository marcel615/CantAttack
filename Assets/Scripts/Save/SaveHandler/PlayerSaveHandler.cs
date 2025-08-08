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
            position = player.savePosition,

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

        //SystemMenu 패널에서 메인메뉴로 씬 전환 요청하는 경우 이벤트 (세이브하는 경우)
        //SceneTransitionEvents.OnSystemMenuToMainMenu += SavePlayerPos;

        //SavePoint에서 저장하기 직전에 보내는 이벤트
        SystemEvents.OnSavePointNotice += SavePlayerPos;
    }
    private void OnDisable()
    {
        SystemEvents.OnSaveDicKeyRequest -= SaveDicKey;

        //SystemMenu 패널에서 메인메뉴로 씬 전환 요청하는 경우 이벤트 (세이브하는 경우)
        //SceneTransitionEvents.OnSystemMenuToMainMenu -= SavePlayerPos;

        //SavePoint에서 저장하기 직전에 보내는 이벤트
        SystemEvents.OnSavePointNotice -= SavePlayerPos;
    }
    //SaveManager에서 딕셔너리 구성하는 과정
    void SaveDicKey(SaveManager saveManager)
    {
        saveManager.GetDicKey(this);
    }
    void SavePlayerPos(Transform saveTransform)
    {
        player.savePosition = saveTransform.position;
    }

}
