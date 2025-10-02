using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrapEvents
{
    //Trap이 파괴될 때
    public static event Action<GameObject> OnTrapDestroyed;

    //Trap이 파괴될 때
    public static void InvokeTrapDestroyed(GameObject gameObject)
    {
        OnTrapDestroyed?.Invoke(gameObject);
    }

}
