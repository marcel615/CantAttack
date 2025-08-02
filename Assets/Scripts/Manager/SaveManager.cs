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
    //오브젝트 중복체크를 위한 인스턴스 생성
    private static SaveManager Instance;

    //딕셔너리로 SaveData 내 여러 Data분류 가능하게 하기
    Dictionary<string, ISaveLoadable> SaveLoadDic;

    //ISaveLoadable 인터페이스 상속한 오브젝트 개수
    int ISaveLoadableSum;
    int ISaveLoadableCount = 0;


    //테스트인지 확인 
    bool isTestSave;
    int slotCount; //세이브파일 개수 

    //세이브&로드 Path 관련
    string TestSavePath => Path.Combine(Application.dataPath, "TestSaveFolder");  //테스트 경로
    string RealSavePath => Application.persistentDataPath;                        //실제 경로

    //초기 세이브 데이터
    public SaveDataSO testSaveData; //테스트 데이터
    public SaveDataSO realSaveData; //실제 데이터


    //현재 정해진 세이브&로드 Path 
    string NowSavePath;
    //현재 세이브&로드에 사용될 데이터
    SaveData saveData;




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

        //ISaveLoadable 상속한 오브젝트 총 개수 구하기
        ISaveLoadableSum = GetSaveUnitCount();
    }

    //이벤트 구독
    private void OnEnable()
    {
        SystemEvents.OnDataLoadStart += SaveFileLoadStart; //세이브파일 로드 시작하라는 이벤트 구독
        SystemEvents.OnSaveRequest += Save;     //저장하라는 이벤트 구독
        
        SystemEvents.ONNewGameORLatestSave += NewGameORLatestSave; //세이브파일이 없다면 생성하고, 있다면 가장 최근 세이브파일 열도록 하는 이벤트
    }
    private void OnDisable()
    {
        SystemEvents.OnDataLoadStart -= SaveFileLoadStart; //세이브파일 로드 시작하라는 이벤트 구독
        SystemEvents.OnSaveRequest -= Save;     //저장하라는 이벤트 구독

        SystemEvents.ONNewGameORLatestSave -= NewGameORLatestSave; //세이브파일이 없다면 생성하고, 있다면 가장 최근 세이브파일 열도록 하는 이벤트
    }
    /// <Load>
    //세이브파일 로드 시작
    void SaveFileLoadStart(int num)
    {
        isTestSave = GameManager.Instance.isTest;
        //초기값으로 테스트 or 실제 선택하고 NowSavePath 지정 후 CheckSaveFile() 실행
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
    //Json 파일이 경로에 있으면 saveData 할당, 없으면 json파일 생성하고 saveData 할당
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
        //딕셔너리 완성을 위해 이벤트 발행
        SystemEvents.InvokeSaveDicKeyRequested(this);
    }
    //딕셔너리 설정
    public void GetDicKey(ISaveLoadable saveLoadable)
    {
        if (!SaveLoadDic.ContainsKey(saveLoadable.DicKey))
        {
            SaveLoadDic.Add(saveLoadable.DicKey, saveLoadable);
        }
        else
        {
            //SaveLoadDic의 저장된 key가 중복될 경우
        }
        ISaveLoadableCount++;
        //모든 인터페이스 딕셔너리 완성되면 Load() 실행하도록
        if (ISaveLoadableCount == ISaveLoadableSum)
        {
            Load(saveData);
            ISaveLoadableCount = 0;
        }
    }
    //saveData 로드하기
    public void Load(SaveData saveData)
    {
        SaveLoadDic["PlayerSaveHandler"].Load(saveData.playerSaveData);
        SaveLoadDic["SystemSaveHandler"].Load(saveData.systemSaveData);
        SaveLoadDic["MapSaveHandler"].Load(saveData.mapSaveData);


        Debug.Log("로드 완!");
        //Test
        //SceneManager.LoadScene("Map1");
        //데이터 로드 완료했다는 이벤트 발행
        SystemEvents.InvokeDataLoadFinished();
    }
    /// </Load>

    /// <Save>
    //저장하라는 이벤트 들어오면 실행
    void Save()
    {
        saveData = GetSaveData();
        //saveData를 Json파일에 덮어쓰기 작업
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(NowSavePath, json);

        //세이브 끝나면 이벤트 발행
        SystemEvents.InvokeSaveEnd();
    }
    //분산된 SaveData 가져와서 정식 SaveData 만들기
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
    

    //세이브파일이 없다면 생성하고, 있다면 가장 최근 세이브파일 열도록 하는 이벤트
    void NewGameORLatestSave()
    {
        isTestSave = GameManager.Instance.isTest;
        slotCount = GameManager.Instance.slotCount;

        string fileName;
        string filePath;
        int latestIndex = -1;

        DateTime latestTime = DateTime.MinValue;

        //가장 최근 세이브파일이 존재하면 latestIndex값 갱신
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
        //New Game 일 때
        if (latestIndex == -1)
        {
            SystemEvents.InvokeDataLoadStart(1);
        }
        //가장 최근 세이브파일 열 때
        else
        {
            SystemEvents.InvokeDataLoadStart(latestIndex);
        }

    }

    //SaveData 내에 ISaveLoadable 상속한 오브젝트 개수 구하기
    int GetSaveUnitCount()
    {
        var fields = typeof(SaveData).GetFields(BindingFlags.Public | BindingFlags.Instance);
        return fields.Length;
    }

}
