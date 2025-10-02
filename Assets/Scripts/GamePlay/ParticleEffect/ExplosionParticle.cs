using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPrefab : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    private void Awake()
    {
        Destroy(gameObject, 2f);
    }
}
