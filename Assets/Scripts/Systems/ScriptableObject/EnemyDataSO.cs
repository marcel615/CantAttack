using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    //Enemy 기본 정보들
    [Header("Enemy 기본 정보들")]
    public int MaxHP;
    public int CurrentHP;

    //Idle 관련 변수
    [Header("Idle 관련 변수")]
    public float idleMinWaitTime;
    public float idleMaxWaitTime;

    //순찰 관련 변수
    [Header("순찰 관련 변수")]
    public float patrolSpeed;
    public float patrolMinDistance;
    public float patrolMaxDistance;

    //피격되었을 때 관련 변수    
    [Header("피격되었을 때 관련 변수 ")] 
    public bool isKnockbackEnable;

    //플레이어 감지했을 때 관련 변수
    [Header("플레이어 감지했을 때 관련 변수")]
    public float chaseSpeed;

    //공격 관련 변수
    [Header("공격 관련 변수")]
    public int attackDamage;
    public float attackTime;
    public float attackWaitTime;
    public GameObject slashEffectPrefab;

    //스턴 관련 변수
    //[Header("스턴 관련 변수")]

    //죽음 관련 변수
    //[Header("죽음 관련 변수")]

}
