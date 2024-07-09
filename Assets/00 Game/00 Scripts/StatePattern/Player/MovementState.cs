using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : CanMoveState
{
    [SerializeField] private const float _timeToRoll = 0.05f;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.inputSO.buttonJump && _blackboard.onGround)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.startJumpState);
            return;
        }

        if (!_blackboard.onGround)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
            return;
        }

        if (_blackboard.onSlope)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.slopeState);
            return;
        }

        if (_blackboard.inputSO.buttonRoll && _elapsedTime > _timeToRoll 
            && _stateManager.stateReferences.idleActionState._elapsedTime > _stateManager.stateReferences.idleActionState._timeToAttack)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.rollState);
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
