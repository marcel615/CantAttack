using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldSlotHandler : MonoBehaviour
{
    //방패 슬롯 배열
    ShieldDataSO[] shieldDataSOSlots = new ShieldDataSO[4];

    //ShieldDataSO들
    [SerializeField] ShieldDataSO[] shieldDataSOs;

    //현재 선택된 슬롯 추적 변수
    int currentIndex;

    private void OnEnable()
    {
        //세이브데이터 로드가 완료되는 이벤트 구독
        SystemEvents.OnDataLoadFinished += AssignShieldToSlot;
    }
    private void OnDisable()
    {
        //세이브데이터 로드가 완료되는 이벤트 구독
        SystemEvents.OnDataLoadFinished -= AssignShieldToSlot;
    }

    void AssignShieldToSlot()
    {
        int totalNum = shieldDataSOs.Length;
        for(int i = 0; (i < 4 && i < totalNum); i++)
        {
            if (shieldDataSOs[i] != null)
            {
                shieldDataSOSlots[i] = shieldDataSOs[i];
            }
        }
        PlayerEvents.InvokeShieldSlotUpdated(shieldDataSOSlots);
    }



}
