using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleTrigger : MonoBehaviour
{
    //GameEventDataSO
    [SerializeField] private GameEventDataSO gameEventDataSO;
    [SerializeField] private Transform bossTransform;

    //플레이어 감지 변수
    GameObject detectedTarget;

    //게임이벤트 관련
    bool isCompleted;
    bool isStarted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어 감지 시 detectedTarget에 저장
        if (collision.CompareTag("Player"))
        {
            detectedTarget = collision.gameObject;
            isCompleted = GameEventManager.Instance.isGameEventCompleted(gameEventDataSO.gameEventID);
            if (!isCompleted && !isStarted)
            {
                isStarted = true;
                GameEvents.InvokeBossFightStart(gameEventDataSO.gameEventID, bossTransform);
            }
        }
    }
}
