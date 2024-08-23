using UnityEngine;

public class EnemyAttackState : EnemyNormalState
{
    //[SerializeField] protected ParticleSystem _attackEffect;

    public override void EnterState()
    {
        base.EnterState();
        //_attackEffect.Play();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.enemyController.hitAttackType == AttackType.NormalAttack)
        {
            _stateManager.ChangeState(_stateReferences.enemyHitState);
            return StateStatus.Success;
        }

        if (_blackboard.enemyController.hitAttackType == AttackType.HeavyAttack)
        {
            _stateManager.ChangeState(_stateReferences.enemyKnockDownState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public virtual void Attack(AttackType attackType)
    {
        if (_blackboard.enemyController.target == null) return;

        var targetComponent = _blackboard.enemyController.target.GetComponent<IHitable>();

        var hitEffect = _blackboard.enemyController.attackHitEffect;
        hitEffect.Spawn(_blackboard.enemyController.target.transform.position + Vector3.up, Quaternion.identity, null);

        var damage = RPGObject.RandomDamage(_blackboard.enemyController.enemyData.attackDamage);
        targetComponent.OnHit(damage, attackType);
    }
}
