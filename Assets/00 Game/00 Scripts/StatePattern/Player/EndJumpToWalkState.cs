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
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.movement == Vector3.zero  && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleNormalState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.character.GetSpeed() < 6f && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.moveState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.character.GetSpeed() >= 6f && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.runState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
