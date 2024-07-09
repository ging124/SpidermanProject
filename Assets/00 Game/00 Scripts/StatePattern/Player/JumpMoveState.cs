
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneMoveState : CanMoveState
{
    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.inputSO.move != Vector2.zero)
        {
            Movement(_blackboard.playerData.moveSpeed);
        }

        if (_blackboard.inputSO.buttonRun)
        {
            Movement(_blackboard.playerData.moveSpeed + _blackboard.playerData.speedBoost);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
