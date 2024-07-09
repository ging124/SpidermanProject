using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMoveState : MovementState
{
    [SerializeField] private ClipTransition _stopMoveAnim;
    [SerializeField] private float _timeChangeState = 0.5f;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_stopMoveAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        Movement(_blackboard.playerData.moveSpeed);

        if (_blackboard.inputSO.move == Vector2.zero && _elapsedTime > _timeChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleState);
            return;
        }

        if(_blackboard.inputSO.move != Vector2.zero && _elapsedTime > _timeChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.startMoveState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
