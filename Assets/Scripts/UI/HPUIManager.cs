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
    public GameObject HPOnCellPrefab;
    public GameObject HPOffCellPrefab;

    //���� HP ����
    int CurrentHP;
    int MaxHP;



    private void Awake()
    {
        //�ڽ� ������Ʈ�� �ν����Ϳ��� ���� ��Ծ��� ��쿡 ���
        if (Portrait == null) Portrait = transform.Find("Portrait")?.gameObject;
        if (HPContainer == null) HPContainer = transform.Find("HPContainer")?.gameObject;


    }

    //�̺�Ʈ ����
    private void OnEnable()
    {
        //�÷��̾� ���� �̺�Ʈ ����
        PlayerEvents.OnPlayerSpawned_HPUIManager += SetHP;
        //�÷��̾� ������ �̺�Ʈ ����
        PlayerEvents.OnPlayerDamaged_HPUIManager += SetHP;
    }
    private void OnDisable()
    {
        //�÷��̾� ���� �̺�Ʈ ����
        PlayerEvents.OnPlayerSpawned_HPUIManager -= SetHP;
        //�÷��̾� ������ �̺�Ʈ ����
        PlayerEvents.OnPlayerDamaged_HPUIManager -= SetHP;
    }
    //HPĭ ä���
    void SetHP(int maxHP, int currentHP)
    {
        //�̺�Ʈ�� ���� ���� ��������
        MaxHP = maxHP;
        CurrentHP = currentHP;

        //�ϴ� HPPrefab�� �� ����
        foreach (Transform child in HPContainer.transform)
        {
            Destroy(child.gameObject);
        }
        //HPPrefab ä���
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
