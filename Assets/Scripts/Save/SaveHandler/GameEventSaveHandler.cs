using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSaveHandler : MonoBehaviour, ISaveLoadable
{
    //세이브데이터 저장 및 불러오기 대상들
    [SerializeField] GameEventManager gameEventManager;

    private void Awake()
    {
        //내 컴포넌트 연결
        gameEventManager = GetComponent<GameEventManager>();
    }

    //ISaveLoadable 인터페이스 구현
    public string DicKey => "GameEventSaveHandler";

    public object Save()
    {
        return new GameEventSaveData
        {
            completedGameEventIDs = gameEventManager.completedGameEventIDs,
        };
    }
    public void Load(object saveData)
    {
        GameEventSaveData gameEventSaveData = saveData as GameEventSaveData;
        if (gameEventSaveData != null)
        {
            gameEventManager.completedGameEventIDs = gameEventSaveData.completedGameEventIDs;
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
