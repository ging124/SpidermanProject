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

        PlayerDetection();

        if (_blackboard.enemyController.onHit)
        {
            _stateManager.ChangeState(_stateReferences.enemyHitState);
            return StateStatus.Success;
        }

        if (_blackboard.enemyController.followPlayer == true && _stateManager.currentState != _stateReferences.enemyRunState)
        {
            _stateManager.ChangeState(_stateReferences.enemyRunState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public void PlayerDetection()
    {
        Collider[] playerDetection = Physics.OverlapSphere(_blackboard.enemyController.transform.position, _blackboard.enemyController.enemyData.dectectionRange, _blackboard.enemyController.enemyData.playerLayer);
        if (playerDetection.Length != 0)
        {
            _blackboard.enemyController.followPlayer = true;
        }
        else
        {
            _blackboard.enemyController.followPlayer = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_blackboard.enemyController.transform.position, _blackboard.enemyController.enemyData.dectectionRange);
    }
}
