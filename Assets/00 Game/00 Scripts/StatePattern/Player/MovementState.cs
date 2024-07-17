using Animancer;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class MovementState : CanMoveState
{
    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
            return;
        }

        if (_blackboard.wallFront && _blackboard.wallInHead)
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
