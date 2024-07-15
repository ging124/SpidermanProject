using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAirState : AirborneMoveState
{
    [SerializeField] private ClipTransition _onAirAnim;
    [SerializeField] private float _timeToChangeState = 0.5f;
    [SerializeField] private float _timeToDive = 0.5f;


    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_onAirAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_blackboard.character.fallingTime > _timeToDive)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.diveState);
            return;
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
}
