using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static InventoryManager Instance;

    //플레이어가 획득한 방패 인벤토리
    public List<ShieldDataSO> shieldInventory;
    public IReadOnlyList<ShieldDataSO> GetShieldInventory() => shieldInventory;

    //플레이어가 획득한 패리모드 인벤토리
    public List<ParryModeDataSO> parryModeInventory;
    public IReadOnlyList<ParryModeDataSO> GetParryModeInventory() => parryModeInventory;


    private void Awake()
    {
        // 기존 인스턴스가 존재할 때 && 지금 새로운 인스턴스가 생성되려고 할 때
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //중복되지 않도록 지금 새롭게 생성되는 놈은 파괴시킨다
            return;
        }
        // 인스턴스 처음 할당
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }




}
