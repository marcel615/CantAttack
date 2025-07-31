using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private PortalDataSO portalDataSO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Context 변경 이벤트
            InputEvents.InvokeContextUpdate(InputContext.SceneChange, true);
            //Portal에 들어왔을 때
            PortalEvents.InvokePortalEnter(portalDataSO.portalID, portalDataSO.targetScene, portalDataSO.targetPortalID);
        }
    }
}
