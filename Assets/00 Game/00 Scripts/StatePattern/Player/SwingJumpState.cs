using Animancer;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingJumpState : AirborneMoveState
{
    [SerializeField] private ClipTransition _swingJumpAnim;
    [SerializeField] private float _swingJumpForce;
    [SerializeField] private float _timeToChangeState = 0.5f;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_swingJumpAnim);
        _blackboard.character.AddForce(_blackboard.character.GetVelocity() * _swingJumpForce);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
