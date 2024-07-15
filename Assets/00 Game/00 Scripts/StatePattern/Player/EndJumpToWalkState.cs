using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndJumpToWalkState : AirborneMoveState
{
    [SerializeField] private ClipTransition _endJumpToWalkAnim;
    [SerializeField] private float _timeToChangeState = 0.5f;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_endJumpToWalkAnim);
        _blackboard.character.StopJumping();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.character.IsGrounded() && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleNormalState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.inputSO.buttonJump && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.startJumpState);
            return;
        }

        if (_blackboard.inputSO.move != Vector2.zero && _blackboard.character.IsGrounded() && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.moveState);
            return;
        }

        if (_blackboard.inputSO.buttonJump && _blackboard.character.IsGrounded() && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.startJumpState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
