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
            //Context ���� �̺�Ʈ
            InputEvents.InvokeContextUpdate(InputContext.SceneChange, true);
            //Portal�� ������ ��
            PortalEvents.InvokePortalEnter(portalDataSO.portalID, portalDataSO.targetScene, portalDataSO.targetPortalID);
        }
    }
}
