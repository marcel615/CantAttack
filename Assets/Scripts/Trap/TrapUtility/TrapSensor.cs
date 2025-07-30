using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSensor : MonoBehaviour
{
    //�÷��̾� ���� ����
    public GameObject detectedTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�÷��̾� ���� �� detectedTarget�� ����
        if (collision.CompareTag("Player"))
        {
            detectedTarget = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //�÷��̾� ���� �� detectedTarget�� ����Ǿ� �ִ� �� ����
        if (collision.CompareTag("Player") && collision.gameObject == detectedTarget)
        {
            detectedTarget = null;
        }
    }

}
