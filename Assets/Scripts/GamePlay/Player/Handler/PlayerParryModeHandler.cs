using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryModeHandler : MonoBehaviour
{
    //패리 모드 슬롯 배열
    public ParryModeDataSO[] parryModeSlot = new ParryModeDataSO[4];

    //ParryModeDataSO들
    [SerializeField] ParryModeDataSO[] parryModeDataSOs;

    //현재 선택된 슬롯 추적 변수
    int currentIndex;
    ParryModeDataSO currentParryModeDataSO;

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
        int totalNum = parryModeDataSOs.Length;
        int minIndex = 4;
        for (int i = 0; (i < 4 && i < totalNum); i++)
        {
            if (parryModeDataSOs[i] != null)
            {
                parryModeSlot[i] = parryModeDataSOs[i];
                //게임 시작 시 임시로 일단 제일 작은 인덱스 슬롯을 현재 선택한 것으로 치기
                if (i < minIndex)
                {
                    minIndex = i;
                    ParryModeSlotSelected(i);
                }
            }
        }
        PlayerEvents.InvokeParryModeSlotUpdated(parryModeSlot);
    }
    void ParryModeSlotSelected(int index)
    {
        if (parryModeSlot[index] != null && parryModeSlot[index].parryModeType != ParryModeType.Empty)
        {
            currentIndex = index;
            currentParryModeDataSO = parryModeSlot[index];
            Debug.Log(currentParryModeDataSO.parryModeType + " is Selected");
            PlayerEvents.InvokeCurrentParryModeUpdated(currentParryModeDataSO, currentIndex);
        }
    }

    //ParryMode 인벤토리에서 슬롯 교체 요청할 때
    void OnRequestEquipParryMode(ParryModeDataSO parryModeData, int index)
    {
        parryModeSlot[index] = parryModeData;
        PlayerEvents.InvokeParryModeSlotUpdated(parryModeSlot);
    }

    //현재 패리 모드 슬롯 내보내기
    public ParryModeDataSO[] GetParryModeSlots()
    {
        return parryModeSlot;
    }
}
