using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSaveHandler : MonoBehaviour, ISaveLoadable
{
    //세이브데이터 저장 및 불러오기 대상들
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        //내 컴포넌트 연결
        gameManager = GetComponent<GameManager>();
    }

    //ISaveLoadable 인터페이스 구현
    public string DicKey => "SystemSaveHandler";

    public object Save()
    {
        return new SystemSaveData
        {
            //slotCount = gameManager.slotCount,
        };
    }
    public void Load(object saveData)
    {
        SystemSaveData systemSaveData = saveData as SystemSaveData;
        if (systemSaveData != null)
        {
            //gameManager.slotCount = systemSaveData.slotCount;
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
