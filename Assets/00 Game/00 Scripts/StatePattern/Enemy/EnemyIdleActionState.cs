using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleActionState : EnemyActionState
{
    public override void EnterState(EnemyStateManager stateManager, EnemyBlackboard enemyBlackboard)
    {
        base.EnterState(stateManager, enemyBlackboard);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_blackboard.enemyController.canAttack 
            && ((EnemyStateManagerAction)_stateManager).stateManagerMovement.currentState != _stateManager.stateReferences.enemyHitState
            && ((EnemyStateManagerAction)_stateManager).stateManagerMovement.currentState != _stateManager.stateReferences.enemyDeadState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.enemyAttack1State);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
