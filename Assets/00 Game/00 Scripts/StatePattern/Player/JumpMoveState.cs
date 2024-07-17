
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
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        Movement();

        if (_blackboard.wallFront)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.climbState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
