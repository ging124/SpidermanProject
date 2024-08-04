using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockDownState : EnemyNormalState
{
    [SerializeField] private ClipTransition _knockDownAnim;
    [SerializeField] private float _timeToStandUp = 0.1f;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_knockDownAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_elapsedTime > _timeToStandUp)
        {
            //_stateManager.ChangeState(_stateReferences.enemyDeadState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
