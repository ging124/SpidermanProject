using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAirState : AirborneMoveState
{
    [SerializeField] private ClipTransition _onAirAnim;
    [SerializeField] private float _timeToChangeState = 0.5f;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_onAirAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_blackboard.onSlope)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.slopeState);
        }

        if (_blackboard.onGround && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
