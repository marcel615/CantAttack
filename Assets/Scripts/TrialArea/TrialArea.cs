using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialArea : MonoBehaviour
{
    public List<GameObject> trialObjects;
    int trialObjCount = 0;


    private void OnEnable()
    {
        //Trap이 파괴될 때
        TrapEvents.OnTrapDestroyed += OnTrapDestroyed;
    }   
    private void OnDisable()
    {
        //Trap이 파괴될 때
        TrapEvents.OnTrapDestroyed -= OnTrapDestroyed;
    }
    void OnTrapDestroyed(GameObject gameObject)
    {
        if (trialObjects.Contains(gameObject))
        {
            trialObjCount++;
            if(trialObjCount == trialObjects.Count)
            {
                GameEvents.InvokeTrialAreaEnd();
            }
        }
    }

}
