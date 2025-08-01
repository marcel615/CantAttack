using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sequences;


[CreateAssetMenu(fileName = "MapData_")]
public class MapDataSO : ScriptableObject
{
    public string mapID;                // "MAP_001"
    public string mapName;              // "���� �Ա�"
    public SceneReference scene;        // UnityEngine.SceneManagement.Scene �̸� ����
    public string sceneName;            // �ش� ���� ����ִ� �� �̸�
    public AudioClip bgm;
}
