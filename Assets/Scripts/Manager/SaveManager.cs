using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    private static SaveManager Instance;

    /// <초기값세팅관련>
    //테스트인지 확인 
    bool isTestSave;
    //테스트 세이브 관련
    public SaveDataSO testSaveData;
    string TestSaveFileName = "TestSaveFile.json";
    //string TestSavePath => Path.Combine(Application.persistentDataPath, TestSaveFileName);
    string TestSavePath => Path.Combine(Application.dataPath, "TestSaveFolder", TestSaveFileName);
    //실제 세이브 관련
    public SaveDataSO firstSaveData;
    string SaveFileName = "SaveFile.json";
    string SavePath => Path.Combine(Application.persistentDataPath, SaveFileName);
    //초기값 세이브&로드 Path관련 
    string NowSavePath;
    /// </초기값세팅관련>

    //세이브 및 로드에 사용될 데이터
    SaveData saveData;

    //딕셔너리로 SaveData 내 여러 Data분류 가능하게 하기
    Dictionary<string, ISaveLoadable> SaveLoadDic;

    //ISaveLoadable 상속한 오브젝트 개수
    int ISaveLoadableSum;
    int ISaveLoadableCount = 0;



    private void Awake()
    {
        // 기존 인스턴스가 존재할 때 && 지금 새로운 인스턴스가 생성되려고 할 때
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //중복되지 않도록 지금 새롭게 생성되는 놈은 파괴시킨다
            return;
        }
        // 인스턴스 처음 할당
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //딕셔너리 초기화
        SaveLoadDic = new Dictionary<string, ISaveLoadable>();
        //Json파일 경로 선택 및 saveData 할당
        CheckTestOrNot();

    }
    private void Start()
    {
        //ISaveLoadable 상속한 오브젝트 총 개수 구하기
        ISaveLoadableSum = GetSaveUnitCount();
        //딕셔너리 완성을 위해 이벤트 발행
        SystemEvents.InvokeSaveDicKeyRequested(this);
    }

    //딕셔너리 설정
    public void GetDicKey(ISaveLoadable saveLoadable)
    {
        if (!SaveLoadDic.ContainsKey(saveLoadable.DicKey))
        {
            SaveLoadDic.Add(saveLoadable.DicKey, saveLoadable);

            //모든 인터페이스 딕셔너리 완성되면 Load() 실행하도록
            ISaveLoadableCount++;
            if(ISaveLoadableCount == ISaveLoadableSum)
            {
                Load(saveData);
            }

        }
        else
        {
            //만약에 key가 중복될 경우 디버깅
            Debug.LogWarning($"중복된 DicKey: {saveLoadable.DicKey}");
        }
    }

    //각 SaveData 가져와서 SaveData 만들기
    public SaveData GetSaveData()
    {
        return new SaveData
        {
            playerSaveData = (PlayerSaveData)SaveLoadDic["PlayerSaveHandler"].Save()

        };

    }

    //saveData 로드하기
    public void Load(SaveData saveData)
    {
        SaveLoadDic["PlayerSaveHandler"].Load(saveData.playerSaveData);
        Debug.Log("로드 완!");
        SystemEvents.InvokeDataLoadFinished();
    }

    //저장하라는 이벤트 구독
    private void OnEnable()
    {
        SystemEvents.OnSaveRequest += save;
    }
    private void OnDisable()
    {
        SystemEvents.OnSaveRequest -= save;
    }

    //현재 테스트인지 실제인지 확인
    void CheckTestOrNot()
    {
        isTestSave = GameManager.Instance.isTest;
        //초기값으로 테스트 or 실제 선택
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
    //Json 파일이 경로에 없으면 생성하고 saveData 할당, 있으면 saveData 할당만 하기
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
    //저장하라는 이벤트 들어오면 실행
    void save()
    {
        saveData = GetSaveData();
        //saveData를 Json파일에 덮어쓰기 작업
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(NowSavePath, json);

        //세이브 끝나면 이벤트 발행
        SystemEvents.InvokeSaveEnd();
    }

    //SaveData 내에 ISaveLoadable 상속한 오브젝트 개수 구하기
    int GetSaveUnitCount()
    {
        var fields = typeof(SaveData).GetFields(BindingFlags.Public | BindingFlags.Instance);
        return fields.Length;
    }

}
