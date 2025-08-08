using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SavePoint")]
public class SavePointSO : ScriptableObject
{
    [Header("세이브포인트 ID")]
    public string savePointID;
    [Header("세이브포인트 Position")]
    public Vector2 position;
    [Header("위치 씬 이름")]
    public string sceneName;
}
