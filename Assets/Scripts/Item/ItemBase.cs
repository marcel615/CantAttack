using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public ItemDataSO itemDataSO;
    public abstract void OnAcquire();
}
