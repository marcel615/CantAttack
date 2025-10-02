using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    //PlayerController.cs
    public int MaxHP;   
    public int CurrentHP;
    public Vector2 position;
    public bool isDoubleJumpUnlocked;
}
