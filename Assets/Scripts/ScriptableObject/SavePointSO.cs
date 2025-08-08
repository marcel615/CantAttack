using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SavePoint")]
public class SavePointSO : ScriptableObject
{
    [Header("���̺�����Ʈ ID")]
    public string savePointID;
    [Header("���̺�����Ʈ Position")]
    public Vector2 position;
    [Header("��ġ �� �̸�")]
    public string sceneName;
}
