using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryModeHandler : MonoBehaviour
{
    //패리 모드 슬롯 배열
    ParryModeDataSO[] currentParryModeSlots = new ParryModeDataSO[4];

    //현재 선택된 슬롯 추적 변수
    public int currentIndex;
    ParryModeDataSO currentParryModeDataSO;

    //세이브로드 시 이용할 방패 Type 배열
    public ParryModeType[] currentParryModeSlotsType = new ParryModeType[4];

    private void OnEnable()
    {
        //세이브데이터 로드가 완료되는 이벤트 구독
        SystemEvents.OnDataLoadFinished += AssignParryModeToSlot;
        //ParryModeSlot 변경 요청이 들어올 때
        PlayerEvents.OnParryModeSlotSelected += ParryModeSlotSelected;

        //ParryMode 인벤토리에서 슬롯 교체 요청할 때
        PlayerEvents.OnRequestEquipParryMode += OnRequestEquipParryMode;
    }
    private void OnDisable()
    {
        //세이브데이터 로드가 완료되는 이벤트 구독
        SystemEvents.OnDataLoadFinished -= AssignParryModeToSlot;
        //ParryModeSlot 변경 요청이 들어올 때
        PlayerEvents.OnParryModeSlotSelected -= ParryModeSlotSelected;

        //ParryMode 인벤토리에서 슬롯 교체 요청할 때
        PlayerEvents.OnRequestEquipParryMode -= OnRequestEquipParryMode;
    }

    void AssignParryModeToSlot()
    {
        for (int i = 0; i < currentParryModeSlotsType.Length; i++)
        {
            currentParryModeSlots[i] = ParryModeDatabaseSO.Instance.GetParryModeDataSOByType(currentParryModeSlotsType[i]);
        }

        ParryModeSlotSelected(currentIndex);
        PlayerEvents.InvokeParryModeSlotUpdated(currentParryModeSlots);
    }
    void ParryModeSlotSelected(int index)
    {
        if (currentParryModeSlots[index] != null)
        {
            currentIndex = index;
            currentParryModeDataSO = currentParryModeSlots[index];
            Debug.Log(currentParryModeDataSO.parryModeType + " is Selected");
            PlayerEvents.InvokeCurrentParryModeUpdated(currentParryModeDataSO, currentIndex);
        }
    }

    //ParryMode 인벤토리에서 슬롯 교체 요청할 때
    void OnRequestEquipParryMode(ParryModeDataSO parryModeData, int index)
    {
        currentParryModeSlotsType[index] = parryModeData.parryModeType;
        currentParryModeSlots[index] = parryModeData;
        if (index == currentIndex)
        {
            currentParryModeDataSO = currentParryModeSlots[currentIndex];
            PlayerEvents.InvokeCurrentParryModeUpdated(currentParryModeDataSO, currentIndex);
        }
        PlayerEvents.InvokeParryModeSlotUpdated(currentParryModeSlots);
    }

    //현재 패리 모드 슬롯 내보내기
    public ParryModeDataSO[] GetParryModeSlots()
    {
        return currentParryModeSlots;
    }
}
