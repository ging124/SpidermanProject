using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunLockState : EnemyNormalState
{
    [SerializeField] private ClipTransition _hitAnimList;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.enemyController.agent.enabled = false;
        _normalBodyLayer.Play(_hitAnimList, 0.25f, FadeMode.FromStart);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_elapsedTime > _blackboard.enemyController.stunLockDuration)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.enemyController.stunLockDuration = 0;
        _blackboard.enemyController.agent.enabled = true;
        base.ExitState();
    }
}
