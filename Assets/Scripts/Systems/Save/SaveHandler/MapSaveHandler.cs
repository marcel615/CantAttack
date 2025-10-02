using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSaveHandler : MonoBehaviour, ISaveLoadable
{
    //세이브데이터 저장 및 불러오기 대상들
    [SerializeField] MapManager mapManager;

    private void Awake()
    {
        //내 컴포넌트 연결
        mapManager = GetComponent<MapManager>();
    }

    //ISaveLoadable 인터페이스 구현
    public string DicKey => "MapSaveHandler";

    public object Save()
    {
        return new MapSaveData
        {
            saveScene = mapManager.saveScene,
        };
    }
    public void Load(object saveData)
    {
        MapSaveData mapSaveData = saveData as MapSaveData;
        if (mapSaveData != null)
        {
            mapManager.saveScene = mapSaveData.saveScene;
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
