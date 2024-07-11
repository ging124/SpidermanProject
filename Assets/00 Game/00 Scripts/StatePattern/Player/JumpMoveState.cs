
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
            Movement();
        }

        if (_blackboard.inputSO.buttonRun)
        {
            Movement();
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
