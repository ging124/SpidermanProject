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
            if (Vector3.Distance(_blackboard.enemyController.transform.position, _blackboard.enemyController.target.transform.position) >= _blackboard.enemyController.enemyData.attackRange)
            {
                _stateManager.ChangeState(_stateReferences.enemyRunState);
                return StateStatus.Success;
            }
        }


        if (_blackboard.enemyController.enemyData.GetType() == typeof(Boss) && _blackboard.enemyController.target != null)
        {
            float random = Random.Range(0, 1f);
            if (random < 0.5)
            {
                int randomSkill = Random.Range(0, _stateReferences.enemySkillState.listSkill.Length);
                if (_stateReferences.enemySkillState.listSkill[randomSkill].CanSkill(_blackboard.enemyController.target, _blackboard.enemyController.transform))
                {
                    _stateReferences.enemySkillState.ChoseSkill(randomSkill);
                    _stateManager.ChangeState(_stateReferences.enemySkillState);
                    return StateStatus.Success;
                }
            }
            else
            {
                if (_blackboard.enemyController.canAttack)
                {
                    _stateManager.ChangeState(_stateReferences.enemyAttackState);
                    return StateStatus.Success;
                }
            }
        }
        else if (_blackboard.enemyController.enemyData.GetType() == typeof(RangeEnemy) && _blackboard.enemyController.target != null)
        {
            if (_blackboard.enemyController.canAttack)
            {
                _stateManager.ChangeState(_stateReferences.enemyAttackState);
                return StateStatus.Success;
            }
            else
            {
                _stateManager.ChangeState(_stateReferences.enemyAimState);
                return StateStatus.Success;
            }
        }
        else
        {
            if (_blackboard.enemyController.canAttack)
            {
                _stateManager.ChangeState(_stateReferences.enemyAttackState);
                return StateStatus.Success;
            }
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
