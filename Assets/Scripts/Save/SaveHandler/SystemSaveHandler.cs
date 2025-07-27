using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSaveHandler : MonoBehaviour, ISaveLoadable
{
    //���̺굥���� ���� �� �ҷ����� ����
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        //�� ������Ʈ ����
        gameManager = GetComponent<GameManager>();
    }

    //ISaveLoadable �������̽� ����
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
