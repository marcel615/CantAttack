using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //테스트 세이브 관련
    public ScriptableObject testSaveData;
    string TestSaveFileName = "TestSaveFile.json";
    string TestSavePath => Path.Combine(Application.persistentDataPath, TestSaveFileName);
    bool isTestSave;

    //실제 세이브 관련
    public ScriptableObject firstSaveData;
    string SaveFileName = "SaveFile.json";
    string SavePath => Path.Combine(Application.persistentDataPath, SaveFileName);

    //딕셔너리로 SaveData 내 여러 Data분류 가능하게 하기
    Dictionary<string, ISaveLoadable> SaveLoadDic;

    //ISaveLoadable 상속한 오브젝트 개수
    int ISaveLoadableSum;
    int ISaveLoadableCount = 0;

    //세이브 기능 시 얻게 되는 세이브데이터
    SaveData saveData;

    private void Start()
    {
        //ISaveLoadable 상속한 오브젝트 총 개수 구하기
        ISaveLoadableSum = GetSaveUnitCount(saveData);
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
    }
    //각 SaveData 가져와서 SaveData 만들기
    public SaveData GetSaveData()
    {
        return new SaveData
        {
            playerSaveData = (PlayerSaveData)SaveLoadDic["PlayerSaveHandler"].Save()

        };

    }

    //내일 세부적으로 구현
    public void Load(SaveData saveData)
    {
        SaveLoadDic["PlayerSaveHandler"].Load(saveData.playerSaveData);
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

    //저장하라는 이벤트 들어오면 실행
    void save()
    {
        saveData = GetSaveData();
        //saveData를 Json파일에 덮어쓰기 작업
    }

    //SaveData 내에 ISaveLoadable 상속한 오브젝트 개수 구하기
    int GetSaveUnitCount(SaveData saveData)
    {
        var fields = typeof(SaveData).GetFields(BindingFlags.Public | BindingFlags.Instance);
        return fields.Length;
    }

}
