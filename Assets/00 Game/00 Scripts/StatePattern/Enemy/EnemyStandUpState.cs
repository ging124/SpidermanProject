using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandUpState : EnemyNormalState
{
    [SerializeField] private ClipTransition _standUpAnim;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_standUpAnim).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
        };
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.enemyController.canHit = true;
        base.ExitState();
    }
}
