using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleActionState : EnemyActionState
{
    public override void EnterState()
    {
        base.EnterState();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.enemyController.canAttack)
        {
            _stateManager.ChangeState(_stateReferences.enemyAttackState);
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
