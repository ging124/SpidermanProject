using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartJumpState : AirborneMoveState
{
    [SerializeField] private ClipTransition _startJumpAnim;
    [SerializeField] private float _timeToOnAir;
    [SerializeField] private float _timeToChangeState = 0.15f;
    [SerializeField] private float _timeToSwing = 0.15f;
    [SerializeField] private float jumpImpulseModifier = 1f;

    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.character.jumpImpulse *= jumpImpulseModifier;
        _normalBodyLayer.Play(_startJumpAnim);
        Jump();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        if (!_blackboard.character.IsGrounded() && _elapsedTime >= _timeToOnAir)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.inputSO.move == Vector2.zero && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.character.GetSpeed() <= 7.5f && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpToWalkState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.character.GetSpeed() >= 7.5f && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpToRunState);
            return;
        }

        if (_blackboard.inputSO.buttonJump && !_blackboard.character.IsGrounded() && _elapsedTime > _timeToSwing)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.swingState);
            return;
        }
    }

    public override void ExitState()
    {
        _blackboard.character.jumpImpulse /= jumpImpulseModifier;
        _blackboard.character.StopJumping();
        base.ExitState();
    }

    void Jump()
    {
        _blackboard.character.Jump();
    }
}
