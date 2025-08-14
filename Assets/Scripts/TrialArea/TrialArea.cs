using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialArea : MonoBehaviour
{
    //GameEventDataSO
    [SerializeField] private GameEventDataSO gameEventDataSO;

    //도전 구역 물리쳐야 할 적들
    public List<GameObject> trialObjects;
    int trialObjCount = 0;

    //보상 관련 변수
    public GameObject rewardObject;


    private void OnEnable()
    {
        //Trap이 파괴될 때
        TrapEvents.OnTrapDestroyed += OnTrapDestroyed;
    }   
    private void OnDisable()
    {
        //Trap이 파괴될 때
        TrapEvents.OnTrapDestroyed -= OnTrapDestroyed;
    }
    void OnTrapDestroyed(GameObject gameObject)
    {
        if (trialObjects.Contains(gameObject))
        {
            trialObjCount++;
            if(trialObjCount == trialObjects.Count)
            {
                TrialAreaEnd();
            }
        }
    }
    void TrialAreaEnd()
    {
        //이 TrialArea 이벤트가 이미 완료된 이벤트면 실행 안하도록
        bool isCompleted = GameEventManager.Instance.isGameEventCompleted(gameEventDataSO.gameEventID);
        if (isCompleted) return;

        //도전 구역(Trial Area) 끝날 때 이벤트 발행
        GameEvents.InvokeTrialAreaEnd(rewardObject);
    }

}
