using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionState : PlayerState
{
    //내 컴포넌트
    Rigidbody2D rigid;

    //Interaction 관련 Controller 변수
    IInteractable interactableTarget;
    InputContext thisContext;

    //Interaction 관련 변수
    bool isCanChange;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public override void Enter()
    {
        interactableTarget = FSM.playerController.interactableTarget;
        thisContext = FSM.playerController.thisContext;

        rigid.velocity = new Vector2(0, rigid.velocity.y);
        isCanChange = false;

        if (interactableTarget == null)
        {
            isCanChange = true;
            return;
        }
        interactableTarget.Interact();        
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
    }
    public override bool CanChangeState(PlayerState newState)
    {
        return (isCanChange && base.CanChangeState(newState));
    }

    //이벤트 구독
    private void OnEnable()
    {
        InputEvents.Dialogue.OnDialogueClose += OnDialogueClose;
        SystemEvents.OnSaveEnd += OnSaveEnd;
    }
    private void OnDisable()
    {
        InputEvents.Dialogue.OnDialogueClose -= OnDialogueClose;
        SystemEvents.OnSaveEnd -= OnSaveEnd;
    }
    void OnDialogueClose()
    {
        InputEvents.InvokeContextUpdate(thisContext);
        isCanChange = true;
    }
    void OnSaveEnd()
    {
        //체력 풀로 채우고
        FSM.playerController.CurrentHP = FSM.playerController.MaxHP;
        //플레이어 체력 갱신
        PlayerEvents.InvokePlayerSpawned_HPUIManager(FSM.playerController.MaxHP, FSM.playerController.CurrentHP);

        InputEvents.InvokeContextUpdate(thisContext);
        isCanChange = true;
    }

}
