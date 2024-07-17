using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndJumpState : AirborneMoveState
{
    [SerializeField] private ClipTransition _endJumpAnim;
    [SerializeField] private float _timeToChangeState = 0.5f;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_endJumpAnim);
    }   

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.movement == Vector3.zero && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleNormalState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.movement != Vector3.zero && _elapsedTime > _timeToChangeState)
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
