using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartJumpState : OnAirState
{
    [SerializeField] private ClipTransition _startJumpAnim;
    [SerializeField] private float _timeToOnAir;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_startJumpAnim);
        Jump();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (!_blackboard.character.IsGrounded() && _elapsedTime >= _timeToOnAir)
        {
            _stateManager.ChangeState(_stateReferences.fallState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.character.StopJumping();
        base.ExitState();
    }

    void Jump()
    {
        _blackboard.character.Jump();
    }
}
