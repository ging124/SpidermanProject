using UnityEngine;

public class EnemyAttackState : EnemyNormalState
{
    //[SerializeField] protected ParticleSystem _attackEffect;

    public override void EnterState()
    {
        base.EnterState();
        //_attackEffect.Play();
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
