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
        CanAttack();

        if (_blackboard.enemyController.onHit)
        {
            _stateManager.ChangeState(_stateReferences.enemyHitState);
            return StateStatus.Success;
        }

        if (_blackboard.enemyController.stunLockDuration != 0)
        {
            _stateManager.ChangeState(_stateReferences.enemyStunLockState);
            return StateStatus.Success;
        }

        if (_blackboard.enemyController.followPlayer == true 
            && _stateManager.currentState != _stateReferences.enemyRunState
            && Vector3.Distance(_blackboard.enemyController.transform.position, _blackboard.enemyController.player.transform.position) >= 2)
        {
            _stateManager.ChangeState(_stateReferences.enemyRunState);
            return StateStatus.Success;
        }

        if (_blackboard.enemyController.canAttack && _stateManager.currentState.GetType().BaseType != typeof(EnemyAttackState) && _elapsedTime > 0.2f)
        {
            Debug.Log("Attack");
            _stateManager.ChangeState(_stateReferences.enemyAttackState);
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public virtual void CanAttack()
    {
        if (Physics.Raycast(_blackboard.enemyController.transform.position, _blackboard.enemyController.transform.forward, _blackboard.enemyController.enemyData.canAttackMaxDistance, _blackboard.enemyController.enemyData.playerLayer))
        {
            _blackboard.enemyController.canAttack = true;
        }
        else
        {
            _blackboard.enemyController.canAttack = false;
        }
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
        Gizmos.DrawWireSphere(_blackboard.enemyController.transform.position, _blackboard.enemyController.enemyData.canAttackMaxDistance);
    }
}
