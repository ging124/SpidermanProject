using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRunState : GroundState
{
    [SerializeField] private ClipTransition _stopRunAnim;
    [SerializeField] private float _timeChangeState = 0.5f;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_stopRunAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.inputSO.move != Vector2.zero)
        {
            _stateManager.ChangeState(_stateReferences.runState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.move == Vector2.zero && _elapsedTime > _timeChangeState)
        {
            _stateManager.ChangeState(_stateReferences.idleNormalState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
