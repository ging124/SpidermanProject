using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartJumpHighState : AirborneMoveState
{
    [SerializeField] private ClipTransition _startJumpAnim;
    [SerializeField] private float _timeToOnAir;
    [SerializeField] private float jumpImpulseModifier = 1f;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.character.jumpImpulse *= jumpImpulseModifier;
        _normalBodyLayer.Play(_startJumpAnim);
        Jump();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_blackboard.character.IsGrounded() && _elapsedTime >= _timeToOnAir)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
            return;
        }

        if(_blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleNormalState);
            return;
        }

        if (_blackboard.inputSO.buttonSwing)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.swingState);
            return;
        }
    }

    public override void ExitState()
    {
        _blackboard.character.jumpImpulse /= jumpImpulseModifier;
        base.ExitState();
    }

    void Jump()
    {
        if (_blackboard.character.IsGrounded())
        {
            _blackboard.character.Jump();
        }
    }
}
