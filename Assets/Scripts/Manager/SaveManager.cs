using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

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
    }
    private void OnDisable()
    {
        SystemEvents.OnDataLoadStart -= SaveFileLoadStart; //���̺����� �ε� �����϶�� �̺�Ʈ ����
        SystemEvents.OnSaveRequest -= Save;     //�����϶�� �̺�Ʈ ����
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
            ISaveLoadableCount++;
        }
        else
        {
            //���࿡ key�� �ߺ��� ��� �����
            Debug.LogWarning($"�ߺ��� DicKey: {saveLoadable.DicKey}");
        }
        //��� �������̽� ��ųʸ� �ϼ��Ǹ� Load() �����ϵ���
        if (ISaveLoadableCount == ISaveLoadableSum)
        {
            Load(saveData);
        }
    }
    //saveData �ε��ϱ�
    public void Load(SaveData saveData)
    {
        SaveLoadDic["PlayerSaveHandler"].Load(saveData.playerSaveData);

        Debug.Log("�ε� ��!");
        SystemEvents.InvokeDataLoadFinished(); //�� �̺�Ʈ ������ �������� �̺�Ʈ �߻� �� InitFromSaveFileLoad() �޼ҵ� �����ϵ��� ����
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
            playerSaveData = (PlayerSaveData)SaveLoadDic["PlayerSaveHandler"].Save()

        };
    }
    /// </Save>

    //SaveData ���� ISaveLoadable ����� ������Ʈ ���� ���ϱ�
    int GetSaveUnitCount()
    {
        var fields = typeof(SaveData).GetFields(BindingFlags.Public | BindingFlags.Instance);
        return fields.Length;
    }

}
