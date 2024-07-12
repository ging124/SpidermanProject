using Animancer;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartJumpState : AirborneMoveState
{
    [SerializeField] private ClipTransition _startJumpAnim;
    [SerializeField] private float _timeToOnAir;
    [SerializeField] private float _timeToChangeState = 0.15f;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_startJumpAnim);
        Jump();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(!_blackboard.character.IsGrounded() && _elapsedTime >= _timeToOnAir)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
        }

        if (_blackboard.character.IsGrounded() && _blackboard.inputSO.move == Vector2.zero && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpState);
            return;
        }

        if (_blackboard.character.IsGrounded() && !_blackboard.inputSO.buttonRun && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpToWalkState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.inputSO.buttonRun && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpToRunState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    void Jump()
    {
        if(_blackboard.character.IsGrounded())
        {
            _blackboard.character.Jump();
        }
    }
}
