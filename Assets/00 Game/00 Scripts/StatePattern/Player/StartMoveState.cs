using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMoveState : MovementState
{
    [SerializeField] private ClipTransition _startMoveAnim;
    [SerializeField] private float _timeChangeState = 0.5f;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_startMoveAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        Movement(_blackboard.playerData.moveSpeed);

        if (_blackboard.inputSO.move != Vector2.zero && _elapsedTime > _timeChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.moveState);
        }

        if(_blackboard.inputSO.move == Vector2.zero)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
