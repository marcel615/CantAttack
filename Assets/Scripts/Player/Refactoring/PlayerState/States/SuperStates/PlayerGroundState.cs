using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public override void Enter()
    {
        FSM.idleState.Enter();
        FSM.moveState.Enter();
        FSM.jumpState.Enter();
    }
    public override void UpdateState()
    {
        //if(FSM.playerController.isMoveInput)
            FSM.moveState.UpdateState();
        //if (FSM.playerController.isJumpEvent || FSM.playerController.isJumpHoldEvent)
            FSM.jumpState.UpdateState();
        //if (!FSM.playerController.isMoveInput && !FSM.playerController.isJumpEvent && !FSM.playerController.isJumpHoldEvent)
            FSM.idleState.UpdateState();
    }
    public override void FixedUpdateState()
    {
        //if (FSM.playerController.isMoveInput)
            FSM.moveState.FixedUpdateState();
        //if (FSM.playerController.isJumpEvent || FSM.playerController.isJumpHoldEvent)
            FSM.jumpState.FixedUpdateState();
        //if (!FSM.playerController.isMoveInput && !FSM.playerController.isJumpEvent && !FSM.playerController.isJumpHoldEvent)
            FSM.idleState.FixedUpdateState();
    }
    public override void Exit()
    {
        FSM.idleState.Exit();
        FSM.moveState.Exit();
        FSM.jumpState.Exit();
    }
    public override void SetChangeState()
    {
        allowedTransitions.Add(FSM.inAirState);
        allowedTransitions.Add(FSM.dashState);
        allowedTransitions.Add(FSM.parryState);
        allowedTransitions.Add(FSM.hitState);
        allowedTransitions.Add(FSM.deadState);
        allowedTransitions.Add(FSM.portalState);
        allowedTransitions.Add(FSM.interactionState);
    }

}
