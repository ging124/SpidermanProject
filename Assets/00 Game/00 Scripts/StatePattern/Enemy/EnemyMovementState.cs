using TMPro.EditorUtilities;
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

        _blackboard.enemyController.TargetDetection();
        CanAttack();

        switch (_blackboard.enemyController.hitAttackType)
        {
            case AttackType.NormalAttack:
                _stateManager.ChangeState(_stateReferences.enemyHitState);
                return StateStatus.Success;
            case AttackType.HeavyAttack:
                _stateManager.ChangeState(_stateReferences.enemyKnockDownState);
                return StateStatus.Success;
        }

        if (_blackboard.enemyController.stunLockDuration != 0)
        {
            _stateManager.ChangeState(_stateReferences.enemyStunLockState);
            return StateStatus.Success;
        }

        if (_stateManager.currentState != _stateReferences.enemyRunState && _blackboard.enemyController.target != null)
        {
            if(Vector3.Distance(_blackboard.enemyController.transform.position, _blackboard.enemyController.target.transform.position) >= _blackboard.enemyController.enemyData.attackRange)
            {
                _stateManager.ChangeState(_stateReferences.enemyRunState);
                return StateStatus.Success;
            }
        }

        if (_blackboard.enemyController.canAttack && _stateManager.currentState.GetType().BaseType != typeof(EnemyAttackState) && _elapsedTime > 0.2f)
        {
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
        if (_blackboard.enemyController.target == null) return;

        if (Vector3.Distance(_blackboard.enemyController.transform.position, _blackboard.enemyController.target.transform.position) <= _blackboard.enemyController.enemyData.attackRange)
        {
            _blackboard.enemyController.canAttack = true;
        }
        else
        {
            _blackboard.enemyController.canAttack = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_blackboard.enemyController.transform.position, _blackboard.enemyController.enemyData.dectectionRange);
        Gizmos.DrawWireSphere(_blackboard.enemyController.transform.position, _blackboard.enemyController.enemyData.canAttackMaxDistance);
    }
}
