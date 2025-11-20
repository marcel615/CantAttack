using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    //내 컴포넌트
    [SerializeField] CanvasGroup hudCanvasGroup;

    //자식 오브젝트들
    [SerializeField] private GameObject StatusPanel;
    public GameObject Portrait;
    public GameObject HPContainer;
    [SerializeField] private GameObject ShieldSlotPanel;
    [SerializeField] private GameObject ShieldContainer;

    //HP 아이콘 프리팹
    public GameObject HPOnCellPrefab;
    public GameObject HPOffCellPrefab;

    //EmptyShield 아이콘 프리팹
    public GameObject EmptyShieldPrefab;

    //현재 HP 참조
    int CurrentHP;
    int MaxHP;

    //ShieldSlot 하이라이트 관련
    GameObject selectedIcon;


    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (StatusPanel == null) StatusPanel = transform.Find("StatusPanel")?.gameObject;
        if (Portrait == null) Portrait = transform.Find("StatusPanel/Portrait")?.gameObject;
        if (HPContainer == null) HPContainer = transform.Find("StatusPanel/HPContainer")?.gameObject;
        if (ShieldSlotPanel == null) ShieldSlotPanel = transform.Find("ShieldSlotPanel")?.gameObject;
        if (ShieldContainer == null) ShieldContainer = transform.Find("ShieldSlotPanel/ShieldContainer")?.gameObject;
    }

    //이벤트 구독
    private void OnEnable()
    {
        //플레이어 스폰 이벤트 구독
        PlayerEvents.OnPlayerSpawned_HPUIManager += SetHP;
        //플레이어 데미지 이벤트 구독
        PlayerEvents.OnPlayerDamaged_HPUIManager += SetHP;

        //PlayerShieldSlot이 업데이트되었을 때
        PlayerEvents.OnShieldSlotUpdated += SetShieldSlot;
        //ShieldSlot 변경 요청이 들어올 때
        PlayerEvents.OnShieldSlotSelected += HighlightShieldSlot;
    }
    private void OnDisable()
    {
        //플레이어 스폰 이벤트 구독
        PlayerEvents.OnPlayerSpawned_HPUIManager -= SetHP;
        //플레이어 데미지 이벤트 구독
        PlayerEvents.OnPlayerDamaged_HPUIManager -= SetHP;

        //PlayerShieldSlot이 업데이트되었을 때
        PlayerEvents.OnShieldSlotUpdated -= SetShieldSlot;
        //ShieldSlot 변경 요청이 들어올 때
        PlayerEvents.OnShieldSlotSelected -= HighlightShieldSlot;
    }

    //PlayerHUD UI를 보이게 할지 조절하기 위한 메서드
    public void PlayerHUDVisible(bool visible)
    {
        hudCanvasGroup.alpha = visible ? 1f : 0f;
        hudCanvasGroup.interactable = visible;
        hudCanvasGroup.blocksRaycasts = visible;
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

    //PlayerShieldSlot이 업데이트되었을 때
    void SetShieldSlot(ShieldDataSO[] slots)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            foreach (Transform child in ShieldContainer.transform.GetChild(i))
                Destroy(child?.gameObject);

            GameObject icon = slots[i]?.iconPrefab;
            if (icon != null)
                Instantiate(icon, ShieldContainer.transform.GetChild(i));
        }

        /*
        //일단 shieldPrefab들 다 제거
        foreach (Transform child in ShieldContainer.transform)
        {
            Destroy(child.gameObject);
        }
        //shieldPrefab들 채우기
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i] != null)
            {
                Instantiate(slots[i].iconPrefab, ShieldContainer.transform);
            }
            else
            {
                Instantiate(EmptyShieldPrefab, ShieldContainer.transform);                
            }
        }
        */
    }

    //ShieldSlot 변경 요청이 들어올 때
    void HighlightShieldSlot(int index)
    {
        //이전에 선택된 아이콘 하이라이트 초기화
        if(selectedIcon != null) 
            selectedIcon.transform.localScale = Vector3.one;

        //새롭게 선택된 아이콘
        selectedIcon = ShieldContainer.transform.GetChild(index).gameObject;

        //선택된 아이콘 하이라이트
        selectedIcon.transform.localScale = Vector3.one * 1.2f;
    }
}
