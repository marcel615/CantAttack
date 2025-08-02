using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    private static SaveManager Instance;

    //��ųʸ��� SaveData �� ���� Data�з� �����ϰ� �ϱ�
    Dictionary<string, ISaveLoadable> SaveLoadDic;

    //ISaveLoadable �������̽� ����� ������Ʈ ����
    int ISaveLoadableSum;
    int ISaveLoadableCount = 0;


    //�׽�Ʈ���� Ȯ�� 
    bool isTestSave;
    int slotCount; //���̺����� ���� 

    //���̺�&�ε� Path ����
    string TestSavePath => Path.Combine(Application.dataPath, "TestSaveFolder");  //�׽�Ʈ ���
    string RealSavePath => Application.persistentDataPath;                        //���� ���

    //�ʱ� ���̺� ������
    public SaveDataSO testSaveData; //�׽�Ʈ ������
    public SaveDataSO realSaveData; //���� ������


    //���� ������ ���̺�&�ε� Path 
    string NowSavePath;
    //���� ���̺�&�ε忡 ���� ������
    SaveData saveData;




    private void Awake()
    {
        // ���� �ν��Ͻ��� ������ �� && ���� ���ο� �ν��Ͻ��� �����Ƿ��� �� ��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //�ߺ����� �ʵ��� ���� ���Ӱ� �����Ǵ� ���� �ı���Ų��
            return;
        }
        // �ν��Ͻ� ó�� �Ҵ�
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //��ųʸ� �ʱ�ȭ
        SaveLoadDic = new Dictionary<string, ISaveLoadable>();

        //ISaveLoadable ����� ������Ʈ �� ���� ���ϱ�
        ISaveLoadableSum = GetSaveUnitCount();
    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        SystemEvents.OnDataLoadStart += SaveFileLoadStart; //���̺����� �ε� �����϶�� �̺�Ʈ ����
        SystemEvents.OnSaveRequest += Save;     //�����϶�� �̺�Ʈ ����
        
        SystemEvents.ONNewGameORLatestSave += NewGameORLatestSave; //���̺������� ���ٸ� �����ϰ�, �ִٸ� ���� �ֱ� ���̺����� ������ �ϴ� �̺�Ʈ
    }
    private void OnDisable()
    {
        SystemEvents.OnDataLoadStart -= SaveFileLoadStart; //���̺����� �ε� �����϶�� �̺�Ʈ ����
        SystemEvents.OnSaveRequest -= Save;     //�����϶�� �̺�Ʈ ����

        SystemEvents.ONNewGameORLatestSave -= NewGameORLatestSave; //���̺������� ���ٸ� �����ϰ�, �ִٸ� ���� �ֱ� ���̺����� ������ �ϴ� �̺�Ʈ
    }
    /// <Load>
    //���̺����� �ε� ����
    void SaveFileLoadStart(int num)
    {
        isTestSave = GameManager.Instance.isTest;
        //�ʱⰪ���� �׽�Ʈ or ���� �����ϰ� NowSavePath ���� �� CheckSaveFile() ����
        if (isTestSave)
        {
            string fileName = $"TestSaveFile{num}.json";
            NowSavePath = Path.Combine(TestSavePath, fileName);
            CheckSaveFile(testSaveData.saveDataSO);
        }
        else
        {
            string fileName = $"SaveFile{num}.json";
            NowSavePath = Path.Combine(RealSavePath, fileName);
            CheckSaveFile(realSaveData.saveDataSO);
        }

    }
    //Json ������ ��ο� ������ saveData �Ҵ�, ������ json���� �����ϰ� saveData �Ҵ�
    void CheckSaveFile(SaveData saveDataSO)
    {
        if (File.Exists(NowSavePath))
        {
            string json = File.ReadAllText(NowSavePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            string json = JsonUtility.ToJson(saveDataSO, true);
            File.WriteAllText(NowSavePath, json);
            saveData = saveDataSO;
        }
        MakeDicKeyAndLoad();
    }
    void MakeDicKeyAndLoad()
    {
        //��ųʸ� �ϼ��� ���� �̺�Ʈ ����
        SystemEvents.InvokeSaveDicKeyRequested(this);
    }
    //��ųʸ� ����
    public void GetDicKey(ISaveLoadable saveLoadable)
    {
        if (!SaveLoadDic.ContainsKey(saveLoadable.DicKey))
        {
            SaveLoadDic.Add(saveLoadable.DicKey, saveLoadable);
        }
        else
        {
            //SaveLoadDic�� ����� key�� �ߺ��� ���
        }
        ISaveLoadableCount++;
        //��� �������̽� ��ųʸ� �ϼ��Ǹ� Load() �����ϵ���
        if (ISaveLoadableCount == ISaveLoadableSum)
        {
            Load(saveData);
            ISaveLoadableCount = 0;
        }
    }
    //saveData �ε��ϱ�
    public void Load(SaveData saveData)
    {
        SaveLoadDic["PlayerSaveHandler"].Load(saveData.playerSaveData);
        SaveLoadDic["SystemSaveHandler"].Load(saveData.systemSaveData);
        SaveLoadDic["MapSaveHandler"].Load(saveData.mapSaveData);


        Debug.Log("�ε� ��!");
        //Test
        //SceneManager.LoadScene("Map1");
        //������ �ε� �Ϸ��ߴٴ� �̺�Ʈ ����
        SystemEvents.InvokeDataLoadFinished();
    }
    /// </Load>

    /// <Save>
    //�����϶�� �̺�Ʈ ������ ����
    void Save()
    {
        saveData = GetSaveData();
        //saveData�� Json���Ͽ� ����� �۾�
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(NowSavePath, json);

        //���̺� ������ �̺�Ʈ ����
        SystemEvents.InvokeSaveEnd();
    }
    //�л�� SaveData �����ͼ� ���� SaveData �����
    public SaveData GetSaveData()
    {
        return new SaveData
        {
            playerSaveData = (PlayerSaveData)SaveLoadDic["PlayerSaveHandler"].Save(),
            systemSaveData = (SystemSaveData)SaveLoadDic["SystemSaveHandler"].Save(),
            mapSaveData = (MapSaveData)SaveLoadDic["MapSaveHandler"].Save(),

        };
    }
    /// </Save>
    

    //���̺������� ���ٸ� �����ϰ�, �ִٸ� ���� �ֱ� ���̺����� ������ �ϴ� �̺�Ʈ
    void NewGameORLatestSave()
    {
        isTestSave = GameManager.Instance.isTest;
        slotCount = GameManager.Instance.slotCount;

        string fileName;
        string filePath;
        int latestIndex = -1;

        DateTime latestTime = DateTime.MinValue;

        //���� �ֱ� ���̺������� �����ϸ� latestIndex�� ����
        for (int i = 1; i < slotCount + 1; i++)
        {
            if (isTestSave)
            {
                fileName = $"TestSaveFile{i}.json";
                filePath = Path.Combine(TestSavePath, fileName);
            }
            else
            {
                fileName = $"SaveFile{i}.json";
                filePath = Path.Combine(RealSavePath, fileName);
            }
            if (File.Exists(filePath))
            {
                DateTime modifiedTime = File.GetLastWriteTime(filePath);
                if (modifiedTime > latestTime)
                {
                    latestTime = modifiedTime;
                    latestIndex = i;
                }
            }
        }
        //New Game �� ��
        if (latestIndex == -1)
        {
            SystemEvents.InvokeDataLoadStart(1);
        }
        //���� �ֱ� ���̺����� �� ��
        else
        {
            SystemEvents.InvokeDataLoadStart(latestIndex);
        }

    }

    //SaveData ���� ISaveLoadable ����� ������Ʈ ���� ���ϱ�
    int GetSaveUnitCount()
    {
        var fields = typeof(SaveData).GetFields(BindingFlags.Public | BindingFlags.Instance);
        return fields.Length;
    }

}
