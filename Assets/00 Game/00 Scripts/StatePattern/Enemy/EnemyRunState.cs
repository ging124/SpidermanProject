using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyCanMoveState
{
    [SerializeField] ClipTransition _enemyRunAnim;
    private float _timeToIdle;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_enemyRunAnim);
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
