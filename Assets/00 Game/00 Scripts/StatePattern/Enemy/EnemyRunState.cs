using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyCanMoveState
{
    [SerializeField] ClipTransition _enemyRunAnim;

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

        if (_blackboard.enemyController.followPlayer == false)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
            _blackboard.enemyController.agent.SetDestination(_blackboard.enemyController.transform.position);
            return StateStatus.Success;
        }
        else
        {
            _blackboard.enemyController.movement = _blackboard.enemyController.player.transform.position - _blackboard.enemyController.transform.position;
            Movement(_blackboard.enemyController.player.transform.position);
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
