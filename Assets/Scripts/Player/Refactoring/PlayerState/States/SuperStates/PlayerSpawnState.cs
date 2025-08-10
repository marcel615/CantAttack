using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnState : PlayerState
{
    //내 컴포넌트
    Animator animator;

    //Spawn 관련 변수
    bool isCanChange;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Enter()
    {
        isCanChange = false;

        if (FSM.playerController.isSaveSceneLoaded)
            OnSavedSceneLoaded();
        else if (FSM.playerController.isRespawned)
            OnRespawned();

        isCanChange = true;
    }
    public override void UpdateState()
    {
    }
    public override void FixedUpdateState()
    {
    }
    public override void Exit()
    {
    }
    public override void SetChangeState()
    {
        allowedTransitions.Add(FSM.groundState);
        allowedTransitions.Add(FSM.inAirState);
    }
    public override bool CanChangeState(PlayerState newState)
    {
        return (isCanChange && base.CanChangeState(newState));
    }
    //세이브 로드 이후 초기화
    void OnSavedSceneLoaded()
    {
        //플레이어 위치 초기화
        transform.position = FSM.playerController.player.savePosition + new Vector2(1, 1);
        //플레이어 스폰 이벤트 발행
        PlayerEvents.InvokePlayerSpawned_HPUIManager(FSM.playerController.MaxHP, FSM.playerController.CurrentHP);
        //Context 업데이트
        InputEvents.InvokeContextUpdate(InputContext.Player);

        FSM.playerController.isSaveSceneLoaded = false;
    }
    //리스폰 이후 초기화
    void OnRespawned()
    {
        //체력 풀로 채우고
        FSM.playerController.CurrentHP = FSM.playerController.MaxHP;
        //히트박스 다시 키고
        FSM.playerController.playerHitBoxCollider.enabled = true;
        //애니메이션 설정하고
        animator.SetBool("isDead", false);

        //플레이어 위치 초기화
        transform.position = FSM.playerController.player.savePosition + new Vector2(1, 1);
        //플레이어 스폰 이벤트 발행
        PlayerEvents.InvokePlayerSpawned_HPUIManager(FSM.playerController.MaxHP, FSM.playerController.CurrentHP);
        //Context 업데이트
        InputEvents.InvokeContextUpdate(InputContext.Player);

        FSM.playerController.isRespawned = false;
    }

}
