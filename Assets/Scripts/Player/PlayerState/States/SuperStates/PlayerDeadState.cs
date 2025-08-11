using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    //내 컴포넌트
    Rigidbody2D rigid;
    Animator animator;

    //Dead 관련 Controller 변수
    float deadSequenceTime;
    float deadSlowMotionTime;
    float deadSlowMotionTimeScale;
    GameObject bloodEffectPrefab;

    //Dead 관련 변수
    bool isCanChange;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        deadSequenceTime = FSM.playerController.deadSequenceTime;
        deadSlowMotionTime = FSM.playerController.deadSlowMotionTime;
        deadSlowMotionTimeScale = FSM.playerController.deadSlowMotionTimeScale;
        bloodEffectPrefab = FSM.playerController.bloodEffectPrefab;

        isCanChange = false;
        StartCoroutine(DeadSequence());
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
        allowedTransitions.Add(FSM.spawnState);
    }
    public override bool CanChangeState(PlayerState newState)
    {
        return (isCanChange && base.CanChangeState(newState));
    }
    IEnumerator DeadSequence()
    {
        // 사망 연출
        //Context 변경 이벤트
        InputEvents.InvokeContextUpdate(InputContext.PlayerDead);
        //움직임 멈추고
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        //피 이펙트 실행하고
        GameObject blood = Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
        //애니메이션 설정하고
        animator.SetBool("isDead", true);

        //슬로우모션 진행
        Time.timeScale = deadSlowMotionTimeScale;
        yield return new WaitForSecondsRealtime(deadSlowMotionTime);
        Time.timeScale = 1f;

        yield return new WaitForSeconds(deadSequenceTime);

        //씬 전환 실시
        SceneTransitionEvents.InvokeDeadToRespawn();

        isCanChange = true;
    }

}
