using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameEventDataSO gameEventDataSO;

    private void Start()
    {
        if (GameEventManager.Instance.isGameEventCompleted(gameEventDataSO.gameEventID))
        {
            gameObject.SetActive(false);
        }
    }
}
