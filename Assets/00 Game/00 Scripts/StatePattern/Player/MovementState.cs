using Animancer;
using System.Collections;
using System.Collections.Generic;
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

        if (_blackboard.onHit)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.hitState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
