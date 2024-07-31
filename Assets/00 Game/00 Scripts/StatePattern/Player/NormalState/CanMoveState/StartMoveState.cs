using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMoveState : MovementState
{
    [SerializeField] private ClipTransition _startMoveAnim;
    [SerializeField] private float _timeChangeState = 0.5f;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_startMoveAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        Movement();

        if (_blackboard.inputSO.move != Vector2.zero && _elapsedTime > _timeChangeState)
        {
            _stateManager.ChangeState(_stateReferences.moveState);
        }

        if(_blackboard.inputSO.move == Vector2.zero)
        {
            _stateManager.ChangeState(_stateReferences.idleNormalState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
