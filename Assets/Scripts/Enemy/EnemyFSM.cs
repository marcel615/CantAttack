using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    //�� ������Ʈ
    protected EnemyBehavior enemyBehavior;

    //��� ������ ���� State
    EnemyState currentState;

    private void Awake()
    {
        //EnemyType�� ����� Enemy�� �ִٸ� �Ҵ��ϱ�
        enemyBehavior = GetComponent<EnemyBehavior>();
        if(enemyBehavior == null)
        {
            Debug.Log("EnemyBehavior �������� ����");
        }
        //����� Enemy�� Init() ����
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
