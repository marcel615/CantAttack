using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public override void Enter()
    {
        FSM.fallState.Enter();
        FSM.moveState.Enter();
        FSM.jumpState.Enter();
    }
    public override void UpdateState()
    {
        //if (FSM.playerController.isMoveInput)
            FSM.moveState.UpdateState();
        //if (FSM.playerController.isJumpEvent || FSM.playerController.isJumpHoldEvent)
            FSM.jumpState.UpdateState();

        FSM.fallState.UpdateState();
    }
    public override void FixedUpdateState()
    {
        //if (FSM.playerController.isMoveInput)
            FSM.moveState.FixedUpdateState();
        //if (FSM.playerController.isJumpEvent || FSM.playerController.isJumpHoldEvent)
            FSM.jumpState.FixedUpdateState();

        FSM.fallState.FixedUpdateState();
    }
    public override void Exit()
    {
        FSM.fallState.Exit();
        FSM.moveState.Exit();
        FSM.jumpState.Exit();
    }
    public override void SetChangeState()
    {
        allowedTransitions.Add(FSM.groundState);
        allowedTransitions.Add(FSM.dashState);
        allowedTransitions.Add(FSM.parryState);
        allowedTransitions.Add(FSM.hitState);
        allowedTransitions.Add(FSM.deadState);
        allowedTransitions.Add(FSM.portalState);
    }
}
