using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sequences;


[CreateAssetMenu(fileName = "MapData_")]
public class MapDataSO : ScriptableObject
{
    public string mapID;                // "MAP_001"
    public string mapName;              // "숲속 입구"
    public SceneReference scene;        // UnityEngine.SceneManagement.Scene 이름 참조
    public string sceneName;            // 해당 맵이 들어있는 씬 이름
    public AudioClip bgm;
}
