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

        if (_blackboard.enemyController.canAttack
            && ((StateManagerAction)_stateManager).stateManagerMovement.currentState != _stateReferences.enemyHitState
            && ((StateManagerAction)_stateManager).stateManagerMovement.currentState != _stateReferences.enemyDeadState)
        {
            _stateManager.ChangeState(_stateReferences.enemyAttack1State);
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
