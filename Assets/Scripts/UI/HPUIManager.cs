using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIManager : MonoBehaviour
{
    //�ڽ� ������Ʈ��
    public GameObject Portrait;
    public GameObject HPContainer;

    //HP ������ ������
    public GameObject HPPrefab;



    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (Portrait == null) Portrait = transform.Find("Portrait")?.gameObject;
        if (HPContainer == null) HPContainer = transform.Find("HPContainer")?.gameObject;


    }
    void Start()
    {
        
    }

    void Update()
    {

    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //�÷��̾� ���� �̺�Ʈ ����
        PlayerEvents.OnPlayerSpawned_NoArgument += SetHP;
    }
    private void OnDisable()
    {
        //�÷��̾� ���� �̺�Ʈ ����
        PlayerEvents.OnPlayerSpawned_NoArgument -= SetHP;
    }
    void SetHP()
    {
        Instantiate(HPPrefab, HPContainer.transform);
    }
}
