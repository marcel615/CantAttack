using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    //내 컴포넌트
    protected EnemyBehavior enemyBehavior;

    //계속 추적할 현재 State
    EnemyState currentState;

    private void Awake()
    {
        //EnemyType을 상속한 Enemy가 있다면 할당하기
        enemyBehavior = GetComponent<EnemyBehavior>();
        if(enemyBehavior == null)
        {
            Debug.Log("EnemyBehavior 존재하지 않음");
        }
        //연결된 Enemy의 Init() 실행
        //enemyBehavior.Init(this);
    }
    private void Update()
    {

        switch (currentState)
        {
            case EnemyState.Idle:
                enemyBehavior.Idle();

                break;

            case EnemyState.Chase:
                enemyBehavior.DetectAndChasePlayer();

                break;

            case EnemyState.Attack:
                enemyBehavior.Attack();

                break;

            case EnemyState.Evade:
                enemyBehavior.Evade();

                break;

            case EnemyState.Return:
                enemyBehavior.Return();

                break;

            case EnemyState.Hit:
                //enemyBehavior.Hit();

                break;

            case EnemyState.Dead:
                enemyBehavior.Dead();

                break;
        }
    }
    public void ChangeEnemyState(EnemyState state)
    {
        currentState = state;
    }

}
