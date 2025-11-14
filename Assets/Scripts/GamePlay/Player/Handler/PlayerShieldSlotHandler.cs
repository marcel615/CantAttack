using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldSlotHandler : MonoBehaviour
{
    //방패 슬롯 배열
    ShieldDataSO[] currentShieldSlots = new ShieldDataSO[4];

    //현재 선택된 슬롯 추적 변수
    public int currentIndex;
    ShieldDataSO currentShieldDataSO;

    //세이브로드 시 이용할 방패 Type 배열
    public ShieldType[] currentShieldSlotsType = new ShieldType[4];

    private void OnEnable()
    {
        //세이브데이터 로드가 완료되는 이벤트 구독
        SystemEvents.OnDataLoadFinished += AssignShieldToSlot;
        //ShieldSlot 변경 요청이 들어올 때
        PlayerEvents.OnShieldSlotSelected += ShieldSlotSelected;

        //Shield 인벤토리에서 슬롯 교체 요청할 때
        PlayerEvents.OnRequestEquipShield += OnRequestEquipShield;
}
    private void OnDisable()
    {
        //세이브데이터 로드가 완료되는 이벤트 구독
        SystemEvents.OnDataLoadFinished -= AssignShieldToSlot;
        //ShieldSlot 변경 요청이 들어올 때
        PlayerEvents.OnShieldSlotSelected -= ShieldSlotSelected;

        //Shield 인벤토리에서 슬롯 교체 요청할 때
        PlayerEvents.OnRequestEquipShield -= OnRequestEquipShield;
    }

    void AssignShieldToSlot()
    {
        for (int i = 0; i < currentShieldSlotsType.Length; i++)
        {
            currentShieldSlots[i] = ShieldDatabaseSO.Instance.GetShieldDataSOByType(currentShieldSlotsType[i]);
        }

        ShieldSlotSelected(currentIndex);
        PlayerEvents.InvokeShieldSlotUpdated(currentShieldSlots);
    }
    void ShieldSlotSelected(int index)
    {
        if (currentShieldSlots[index] != null)
        {
            currentIndex = index;
            currentShieldDataSO = currentShieldSlots[index];
            Debug.Log(currentShieldDataSO.shieldType + " is Selected");
            PlayerEvents.InvokeCurrentShieldUpdated(currentShieldDataSO, currentIndex);
        }
    }

    //Shield 인벤토리에서 슬롯 교체 요청할 때
    void OnRequestEquipShield(ShieldDataSO shieldData, int index)
    {
        currentShieldSlotsType[index] = shieldData.shieldType;
        currentShieldSlots[index] = shieldData;
        if(index == currentIndex)
        {
            currentShieldDataSO = currentShieldSlots[currentIndex];
            PlayerEvents.InvokeCurrentShieldUpdated(currentShieldDataSO, currentIndex);
        }
        PlayerEvents.InvokeShieldSlotUpdated(currentShieldSlots);
    }

    //현재 방패 슬롯 내보내기
    public ShieldDataSO[] GetShieldSlots()
    {
        return currentShieldSlots;
    }



}
