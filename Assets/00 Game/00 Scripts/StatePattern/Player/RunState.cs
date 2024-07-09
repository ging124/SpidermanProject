using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementState
{
    [SerializeField] private ClipTransition _runAnim;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_runAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        Movement(_blackboard.playerData.moveSpeed + _blackboard.playerData.speedBoost);

        if (_blackboard.inputSO.move != Vector2.zero)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.moveState);
            return;
        }

        if (_blackboard.inputSO.move == Vector2.zero)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.stopMoveState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
