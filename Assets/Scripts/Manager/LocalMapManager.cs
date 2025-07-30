using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMapManager : MonoBehaviour
{
    public CinemachineVirtualCamera CineCamera;
    [SerializeField] private PolygonCollider2D CameraBounds;

    private void Awake()
    {
        MapEvents.InvokeLocalMapManagerInit(this);
    }
}
