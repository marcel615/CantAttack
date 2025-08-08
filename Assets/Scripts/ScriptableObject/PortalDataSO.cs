using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Portal")]
public class PortalDataSO : ScriptableObject
{
    [Header("��ŻID")]
    public string portalID;
    [Header("��Ż�� ���� Position")]
    public Vector2 position;
    [Header("��Ż�� ���� ����")]
    public PortalWalkDirection portalWalkDirection;

    [Header("���� �� �̸�")]
    public string targetScene;
    [Header("���� �Ǵ� ��ŻID")]
    public string targetPortalID;

}
