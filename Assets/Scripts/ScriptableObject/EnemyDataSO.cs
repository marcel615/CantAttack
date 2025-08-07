using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    //Enemy �⺻ ������
    [Header("Enemy �⺻ ������")]
    public int MaxHP;
    public int CurrentHP;

    //Idle ���� ����
    [Header("Idle ���� ����")]
    public float idleMinWaitTime;
    public float idleMaxWaitTime;

    //���� ���� ����
    [Header("���� ���� ����")]
    public float patrolSpeed;
    public float patrolMinDistance;
    public float patrolMaxDistance;

    //�ǰݵǾ��� �� ���� ����    
    [Header("�ǰݵǾ��� �� ���� ���� ")] 
    public bool isKnockbackEnable;

    //�÷��̾� �������� �� ���� ����
    [Header("�÷��̾� �������� �� ���� ����")]
    public float chaseSpeed;

    //���� ���� ����
    [Header("���� ���� ����")]
    public int attackDamage;
    public float attackTime;
    public float attackWaitTime;
    public GameObject slashEffectPrefab;

    //���� ���� ����
    //[Header("���� ���� ����")]

    //���� ���� ����
    //[Header("���� ���� ����")]

}
