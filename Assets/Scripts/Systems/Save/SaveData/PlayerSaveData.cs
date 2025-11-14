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

    //PlayerShieldSlotHandler.cs
    public ShieldType[] currentShieldSlotsType;
    public int currentShieldSlotIndex;

    //PlayerParryModeHandler.cs
    public ParryModeType[] currentParryModeSlotsType;
    public int currentParryModeSlotIndex;
}
