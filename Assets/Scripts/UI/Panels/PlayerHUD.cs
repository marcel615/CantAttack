using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    //자식 오브젝트들
    public GameObject Portrait;
    public GameObject HPContainer;

    //HP 아이콘 프리팹
    public GameObject HPOnCellPrefab;
    public GameObject HPOffCellPrefab;

    //현재 HP 참조
    int CurrentHP;
    int MaxHP;



    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (Portrait == null) Portrait = transform.Find("Portrait")?.gameObject;
        if (HPContainer == null) HPContainer = transform.Find("HPContainer")?.gameObject;


    }

    //이벤트 구독
    private void OnEnable()
    {
        //플레이어 스폰 이벤트 구독
        PlayerEvents.OnPlayerSpawned_HPUIManager += SetHP;
        //플레이어 데미지 이벤트 구독
        PlayerEvents.OnPlayerDamaged_HPUIManager += SetHP;
    }
    private void OnDisable()
    {
        //플레이어 스폰 이벤트 구독
        PlayerEvents.OnPlayerSpawned_HPUIManager -= SetHP;
        //플레이어 데미지 이벤트 구독
        PlayerEvents.OnPlayerDamaged_HPUIManager -= SetHP;
    }
    //HP칸 채우기
    void SetHP(int maxHP, int currentHP)
    {
        //이벤트로 받은 정보 가져오기
        MaxHP = maxHP;
        CurrentHP = currentHP;

        //일단 HPPrefab들 다 제거
        foreach (Transform child in HPContainer.transform)
        {
            Destroy(child.gameObject);
        }
        //HPPrefab 채우기
        for (int i = 0; i < CurrentHP; i++)
        {
            Instantiate(HPOnCellPrefab, HPContainer.transform);
        }
        for (int i = CurrentHP; i < MaxHP; i++)
        {
            Instantiate(HPOffCellPrefab, HPContainer.transform);
        }
    }
}
