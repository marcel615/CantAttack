using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldData")]
public class ShieldDataSO : ScriptableObject
{
    [Header("방패 ID")]
    public string shieldID;
    [Header("방패 타입")]
    public ShieldType shieldType;
    [Header("방패 아이콘")]
    public GameObject iconPrefab;
    [Header("패리 성공 시 투사체 프리팹")]
    public GameObject parryProjectilePrefab;

    public ScatterShieldData scatter;
}

[System.Serializable]
public struct ScatterShieldData
{
    public int pelletCount;
    public float spreadDeg;
    public float jitterDeg;
    public float speedVariance;
}



