using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Portal")]
public class PortalDataSO : ScriptableObject
{
    [Header("포탈ID")]
    public string portalID;
    [Header("포탈별 존재 Position")]
    public Vector2 position;
    [Header("포탈별 진입 방향")]
    public PortalWalkDirection portalWalkDirection;

    [Header("연결 씬 이름")]
    public string targetScene;
    [Header("연결 되는 포탈ID")]
    public string targetPortalID;

}
