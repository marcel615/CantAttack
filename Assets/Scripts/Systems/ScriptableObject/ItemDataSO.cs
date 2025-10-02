using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string description;

}
