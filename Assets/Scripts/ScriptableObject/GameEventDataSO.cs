using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent")]
public class GameEventDataSO : ScriptableObject
{
    public string gameEventID;
    public GameEventType gameEventType;
    public string gameEventName;
    public string description;
}
