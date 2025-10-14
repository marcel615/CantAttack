using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParryModeData")]
public class ParryModeDataSO : ScriptableObject
{
    [Header("패리 모드 ID")]
    public string parryModeID;
    [Header("패리 모드 타입")]
    public ParryModeType parryModeType;
    [Header("패리 모드 아이콘")]
    public GameObject iconPrefab;

    public DirectionalParryModeData directional;

}

[System.Serializable]
public struct DirectionalParryModeData
{
    public float slowModeScale;
    public float confirmDelay;
    public float maxWaitTime;
}
