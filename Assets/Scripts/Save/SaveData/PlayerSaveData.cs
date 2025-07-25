using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    //PlayerStatus.cs
    public int MaxHP;   
    public int CurrentHP;
    //Player.cs
    public Vector2 position;
}
