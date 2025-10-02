using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    int activePriority = 20;
    int inactivePriority = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            virtualCamera.Priority = activePriority;
            CameraEvents.InvokeSwitchCamera(virtualCamera);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            virtualCamera.Priority = inactivePriority;
        }
    }

}
