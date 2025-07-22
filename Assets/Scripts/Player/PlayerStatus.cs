using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //세이브, 로드 변수
    public int MaxHP;
    public int CurrentHP;


    private void OnEnable()
    {
        //세이브 로드 이후 초기화
        SystemEvents.OnDataLoadFinished += InitFromSave;
    }
    private void OnDisable()
    {
        //세이브 로드 이후 초기화
        SystemEvents.OnDataLoadFinished -= InitFromSave;
    }

    //세이브 로드 이후 초기화
    void InitFromSave()
    {

    }
}
