using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 0.25f);
    }

}
