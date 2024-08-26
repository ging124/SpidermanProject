using UnityEngine;

public class EnemyMovementState : EnemyNormalState
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

        if (_blackboard.enemyController.stunLockDuration != 0)
        {
            _stateManager.ChangeState(_stateReferences.enemyStunLockState);
            return StateStatus.Success;
        }

        if (_stateManager.currentState != _stateReferences.enemyRunState && _blackboard.enemyController.target != null)
        {
            if (Vector3.Distance(_blackboard.enemyController.transform.position, _blackboard.enemyController.target.transform.position) >= _blackboard.enemyController.enemyData.attackRange)
            {
                _stateManager.ChangeState(_stateReferences.enemyRunState);
                return StateStatus.Success;
            }
        }

        if (_blackboard.enemyController.target != null)
        {
            _stateManager.ChangeState(_stateReferences.enemyRetreatState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
