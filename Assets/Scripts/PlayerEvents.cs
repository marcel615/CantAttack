using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEvents
{
    //Player가 스폰될 때
    public static event Action<Transform> OnPlayerSpawned;

    //Player의 Start()에서 이벤트 발행
    public static void InvokePlayerSpawned(Transform transform)
    {
        OnPlayerSpawned?.Invoke(transform);
    }
}
