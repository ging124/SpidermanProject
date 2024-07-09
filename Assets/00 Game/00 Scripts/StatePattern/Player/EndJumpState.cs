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

        if (_blackboard.onGround && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleState);
            return;
        }

        if (_blackboard.onGround && _blackboard.inputSO.buttonJump && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.startJumpState);
            return;
        }

        if (_blackboard.inputSO.move != Vector2.zero && _blackboard.onGround && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.moveState);
            return;
        }

        if (_blackboard.inputSO.buttonJump && _blackboard.onGround && _elapsedTime > _timeToChangeState)
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
