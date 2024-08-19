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
        _blackboard.enemyController.agent.enabled = true;
        _normalBodyLayer.Play(_enemyRunAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if(_blackboard.enemyController.target != null)
        {
            if (Vector3.Distance(_blackboard.enemyController.transform.position, _blackboard.enemyController.target.transform.position) < 1.5f)
            {
                _stateManager.ChangeState(_stateReferences.enemyIdleState);
                return StateStatus.Success;
            }
            else
            {
                Vector3 movement = _blackboard.enemyController.target.transform.position;
                movement.y = _blackboard.enemyController.transform.position.y;
                Movement(movement);
            }
        }
        else
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
            return StateStatus.Success;
        }


        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.enemyController.agent.SetDestination(_blackboard.enemyController.transform.position);
        _blackboard.enemyController.agent.enabled = false;
        base.ExitState();
    }
}
