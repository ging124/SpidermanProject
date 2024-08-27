using Animancer;
using UnityEngine;

public class EnemyKnockDownState : EnemyNormalState
{
    [SerializeField] private ClipTransition _knockDownAnim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.enemyController.animancer.Animator.applyRootMotion = true;
        _normalBodyLayer.Play(_knockDownAnim);
        TakeDamage();
        _blackboard.enemyController.canHit = false;
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.enemyController.currentHP <= 0)
        {
            _stateManager.ChangeState(_stateReferences.enemyDeadState);
            return StateStatus.Success;
        }

        if(_normalBodyLayer.CurrentState.NormalizedTime >= 1)
        {
            _stateManager.ChangeState(_stateReferences.enemyStandUpState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.enemyController.animancer.Animator.applyRootMotion = false;
        base.ExitState();
    }

    public void TakeDamage()
    {
        _blackboard.enemyController.characterController.Move((_blackboard.enemyController.transform.position - new Vector3(_blackboard.enemyController.target.transform.position.x, transform.position.y, _blackboard.enemyController.target.transform.position.z)).normalized);

        _blackboard.enemyController.damagePrefab.Spawn(_blackboard.enemyController.transform.position + Vector3.up, _blackboard.enemyController.hitDamage);
        _blackboard.enemyController.currentHP -= _blackboard.enemyController.hitDamage;

        _blackboard.enemyController.uIEnemyBlackboard.enemyHPBar.EnemyHPChange(_blackboard.enemyController.currentHP, _blackboard.enemyController.enemyData.maxHP);

        if (_blackboard.enemyController.currentHP <= 0)
        {
            _blackboard.enemyController.currentHP = 0;
        }
    }
}
