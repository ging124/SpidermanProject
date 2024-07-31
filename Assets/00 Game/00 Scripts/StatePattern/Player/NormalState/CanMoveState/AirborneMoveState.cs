
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneMoveState : CanMoveState
{
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        Movement();

        if (_blackboard.playerController.wallFront)
        {
            _stateManager.ChangeState(_stateReferences.climbState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
