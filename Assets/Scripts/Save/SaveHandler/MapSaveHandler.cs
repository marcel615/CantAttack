using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSaveHandler : MonoBehaviour, ISaveLoadable
{
    //���̺굥���� ���� �� �ҷ����� ����
    [SerializeField] MapManager mapManager;

    private void Awake()
    {
        //�� ������Ʈ ����
        mapManager = GetComponent<MapManager>();
    }

    //ISaveLoadable �������̽� ����
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
    //�̺�Ʈ ����
    private void OnEnable()
    {
        SystemEvents.OnSaveDicKeyRequest += SaveDicKey;
    }
    private void OnDisable()
    {
        SystemEvents.OnSaveDicKeyRequest -= SaveDicKey;
    }
    //SaveManager���� ��ųʸ� �����ϴ� ����
    void SaveDicKey(SaveManager saveManager)
    {
        saveManager.GetDicKey(this);
    }
}
