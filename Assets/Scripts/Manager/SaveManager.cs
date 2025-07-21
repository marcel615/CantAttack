using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //�׽�Ʈ ���̺� ����
    public ScriptableObject testSaveData;
    string TestSaveFileName = "TestSaveFile.json";
    string TestSavePath => Path.Combine(Application.persistentDataPath, TestSaveFileName);
    bool isTestSave;

    //���� ���̺� ����
    public ScriptableObject firstSaveData;
    string SaveFileName = "SaveFile.json";
    string SavePath => Path.Combine(Application.persistentDataPath, SaveFileName);

    //��ųʸ��� SaveData �� ���� Data�з� �����ϰ� �ϱ�
    Dictionary<string, ISaveLoadable> SaveLoadDic;

    //ISaveLoadable ����� ������Ʈ ����
    int ISaveLoadableSum;
    int ISaveLoadableCount = 0;

    //���̺� ��� �� ��� �Ǵ� ���̺굥����
    SaveData saveData;

    private void Start()
    {
        //ISaveLoadable ����� ������Ʈ �� ���� ���ϱ�
        ISaveLoadableSum = GetSaveUnitCount(saveData);
        //��ųʸ� �ϼ��� ���� �̺�Ʈ ����
        SystemEvents.InvokeSaveDicKeyRequested(this);
    }

    //��ųʸ� ����
    public void GetDicKey(ISaveLoadable saveLoadable)
    {
        if (!SaveLoadDic.ContainsKey(saveLoadable.DicKey))
        {
            SaveLoadDic.Add(saveLoadable.DicKey, saveLoadable);

            //��� �������̽� ��ųʸ� �ϼ��Ǹ� Load() �����ϵ���
            ISaveLoadableCount++;
            if(ISaveLoadableCount == ISaveLoadableSum)
            {
                Load(saveData);
            }
            
        }
    }
    //�� SaveData �����ͼ� SaveData �����
    public SaveData GetSaveData()
    {
        return new SaveData
        {
            playerSaveData = (PlayerSaveData)SaveLoadDic["PlayerSaveHandler"].Save()

        };

    }

    //���� ���������� ����
    public void Load(SaveData saveData)
    {
        SaveLoadDic["PlayerSaveHandler"].Load(saveData.playerSaveData);
    }
    //�����϶�� �̺�Ʈ ����
    private void OnEnable()
    {
        SystemEvents.OnSaveRequest += save;
    }
    private void OnDisable()
    {
        SystemEvents.OnSaveRequest -= save;
    }

    //�����϶�� �̺�Ʈ ������ ����
    void save()
    {
        saveData = GetSaveData();
        //saveData�� Json���Ͽ� ����� �۾�
    }

    //SaveData ���� ISaveLoadable ����� ������Ʈ ���� ���ϱ�
    int GetSaveUnitCount(SaveData saveData)
    {
        var fields = typeof(SaveData).GetFields(BindingFlags.Public | BindingFlags.Instance);
        return fields.Length;
    }

}
