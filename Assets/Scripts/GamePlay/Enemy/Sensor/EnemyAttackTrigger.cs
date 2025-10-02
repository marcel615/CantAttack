using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    //플레이어 감지 변수
    public GameObject detectedTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어 감지 시 detectedTarget에 저장
        if (collision.CompareTag("Player"))
        {
            EnemyEvents.InvokeEnemyAttackTriggerEnter(detectedTarget);
            detectedTarget = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //플레이어 감지 시 detectedTarget에 저장되어 있는 값 삭제
        if (collision.CompareTag("Player") && collision.gameObject == detectedTarget)
        {
            EnemyEvents.InvokeEnemyAttackTriggerExit(detectedTarget);
            detectedTarget = null;
        }
    }

}
