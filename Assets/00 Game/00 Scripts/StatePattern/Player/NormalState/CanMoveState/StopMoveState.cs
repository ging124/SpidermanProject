using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMoveState : MovementState
{
    [SerializeField] private ClipTransition _stopMoveAnim;
    [SerializeField] private float _timeChangeState = 0.5f;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_stopMoveAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        if (_blackboard.inputSO.buttonJump && _blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateReferences.jumpState);
            return;
        }

        if (_blackboard.inputSO.move != Vector2.zero)
        {
            _stateManager.ChangeState(_stateReferences.moveState);
            return;
        }

        if (_blackboard.inputSO.move == Vector2.zero && _elapsedTime > _timeChangeState)
        {
            _stateManager.ChangeState(_stateReferences.idleNormalState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
