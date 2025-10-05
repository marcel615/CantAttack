using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldSlotHandler : MonoBehaviour
{
    //방패 슬롯 배열
    ShieldDataSO[] shieldSlot = new ShieldDataSO[4];

    //ShieldDataSO들
    [SerializeField] ShieldDataSO[] shieldDataSOs;

    //현재 선택된 슬롯 추적 변수
    int beforeIndex;
    int currentIndex;
    ShieldDataSO currentShieldDataSO;

    private void OnEnable()
    {
        //세이브데이터 로드가 완료되는 이벤트 구독
        SystemEvents.OnDataLoadFinished += AssignShieldToSlot;
        //ShieldSlot 변경 요청이 들어올 때
        PlayerEvents.OnShieldSlotSelected += ShieldSlotSelected;
    }
    private void OnDisable()
    {
        //세이브데이터 로드가 완료되는 이벤트 구독
        SystemEvents.OnDataLoadFinished -= AssignShieldToSlot;
        //ShieldSlot 변경 요청이 들어올 때
        PlayerEvents.OnShieldSlotSelected -= ShieldSlotSelected;
    }

    void AssignShieldToSlot()
    {
        int totalNum = shieldDataSOs.Length;
        for(int i = 0; (i < 4 && i < totalNum); i++)
        {
            if (shieldDataSOs[i] != null)
            {
                shieldSlot[i] = shieldDataSOs[i];
            }
        }
        PlayerEvents.InvokeShieldSlotUpdated(shieldSlot);

    }
    void ShieldSlotSelected(int index)
    {
        if (shieldSlot[index] != null)
        {
            currentIndex = index;
            currentShieldDataSO = shieldSlot[index];
            Debug.Log(currentShieldDataSO.shieldType + " is Selected");
            PlayerEvents.InvokeCurrentShieldUpdated(currentShieldDataSO, currentIndex);
        }
    }



}
