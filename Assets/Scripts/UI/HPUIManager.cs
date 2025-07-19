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
        PlayerEvents.OnPlayerSpawned_NoArgument += SetHP;
        //�÷��̾� ������ �̺�Ʈ ����
        PlayerEvents.OnPlayerDamaged += SetHP;
    }
    private void OnDisable()
    {
        //�÷��̾� ���� �̺�Ʈ ����
        PlayerEvents.OnPlayerSpawned_NoArgument -= SetHP;
        //�÷��̾� ������ �̺�Ʈ ����
        PlayerEvents.OnPlayerDamaged -= SetHP;
    }
    //HPĭ ä���
    void SetHP()
    {
        //�ϴ� HPPrefab�� �� ����
        foreach (Transform child in HPContainer.transform)
        {
            Destroy(child.gameObject);
        }
        //PlayerStatus�� ����Ǿ� �ִ� Player ���� ��������
        CurrentHP = GameManager.Instance.Player.status.CurrentHP;
        MaxHP = GameManager.Instance.Player.status.MaxHP;
        //CurrentHP�� �µ��� HPPrefab ä���
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
