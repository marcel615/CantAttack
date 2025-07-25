using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //������Ʈ �ߺ�üũ�� ���� �ν��Ͻ� ����
    private static SaveManager Instance;

    /// <�ʱⰪ���ð���>
    //�׽�Ʈ���� Ȯ�� 
    bool isTestSave;
    //�׽�Ʈ ���̺� ����
    public SaveDataSO testSaveData;
    string TestSaveFileName = "TestSaveFile.json";
    //string TestSavePath => Path.Combine(Application.persistentDataPath, TestSaveFileName);
    string TestSavePath => Path.Combine(Application.dataPath, "TestSaveFolder", TestSaveFileName);
    //���� ���̺� ����
    public SaveDataSO firstSaveData;
    string SaveFileName = "SaveFile.json";
    string SavePath => Path.Combine(Application.persistentDataPath, SaveFileName);
    //�ʱⰪ ���̺�&�ε� Path���� 
    string NowSavePath;
    /// </�ʱⰪ���ð���>

    //���̺� �� �ε忡 ���� ������
    SaveData saveData;

    //��ųʸ��� SaveData �� ���� Data�з� �����ϰ� �ϱ�
    Dictionary<string, ISaveLoadable> SaveLoadDic;

    //ISaveLoadable ����� ������Ʈ ����
    int ISaveLoadableSum;
    int ISaveLoadableCount = 0;



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
        //Json���� ��� ���� �� saveData �Ҵ�
        CheckTestOrNot();

    }
    private void Start()
    {
        //ISaveLoadable ����� ������Ʈ �� ���� ���ϱ�
        ISaveLoadableSum = GetSaveUnitCount();
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
        else
        {
            //���࿡ key�� �ߺ��� ��� �����
            Debug.LogWarning($"�ߺ��� DicKey: {saveLoadable.DicKey}");
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

    //saveData �ε��ϱ�
    public void Load(SaveData saveData)
    {
        SaveLoadDic["PlayerSaveHandler"].Load(saveData.playerSaveData);
        Debug.Log("�ε� ��!");
        SystemEvents.InvokeDataLoadFinished();
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

    //���� �׽�Ʈ���� �������� Ȯ��
    void CheckTestOrNot()
    {
        isTestSave = GameManager.Instance.isTest;
        //�ʱⰪ���� �׽�Ʈ or ���� ����
        if (isTestSave)
        {
            NowSavePath = TestSavePath;
            MakeJsonOrNot(testSaveData.saveDataSO);
        }
        else
        {
            NowSavePath = SavePath;
            MakeJsonOrNot(firstSaveData.saveDataSO);
        }
    }
    //Json ������ ��ο� ������ �����ϰ� saveData �Ҵ�, ������ saveData �Ҵ縸 �ϱ�
    void MakeJsonOrNot(SaveData data)
    {
        if (!File.Exists(NowSavePath))
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(NowSavePath, json);

            saveData = data;
        }
        else
        {
            string json = File.ReadAllText(NowSavePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

    }
    //�����϶�� �̺�Ʈ ������ ����
    void save()
    {
        saveData = GetSaveData();
        //saveData�� Json���Ͽ� ����� �۾�
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(NowSavePath, json);

        //���̺� ������ �̺�Ʈ ����
        SystemEvents.InvokeSaveEnd();
    }

    //SaveData ���� ISaveLoadable ����� ������Ʈ ���� ���ϱ�
    int GetSaveUnitCount()
    {
        var fields = typeof(SaveData).GetFields(BindingFlags.Public | BindingFlags.Instance);
        return fields.Length;
    }

}
