using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandUpState : EnemyNormalState
{
    [SerializeField] private ClipTransition _standUpAnim;
    [SerializeField] private float _timeToIdle = 0.1f;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_standUpAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
