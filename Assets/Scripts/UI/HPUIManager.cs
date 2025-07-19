using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIManager : MonoBehaviour
{
    //자식 오브젝트들
    public GameObject Portrait;
    public GameObject HPContainer;

    //HP 아이콘 프리팹
    public GameObject HPPrefab;



    private void Awake()
    {
        //자식 오브젝트들 인스펙터에서 연결 까먹었을 경우에 대비
        if (Portrait == null) Portrait = transform.Find("Portrait")?.gameObject;
        if (HPContainer == null) HPContainer = transform.Find("HPContainer")?.gameObject;


    }
    void Start()
    {
        
    }

    void Update()
    {

    }

    //이벤트 구독
    private void OnEnable()
    {
        //플레이어 스폰 이벤트 구독
        PlayerEvents.OnPlayerSpawned_NoArgument += SetHP;
    }
    private void OnDisable()
    {
        //플레이어 스폰 이벤트 구독
        PlayerEvents.OnPlayerSpawned_NoArgument -= SetHP;
    }
    void SetHP()
    {
        Instantiate(HPPrefab, HPContainer.transform);
    }
}
