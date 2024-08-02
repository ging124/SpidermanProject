using UnityEngine;

public class EnemyAttackState : EnemyActionState
{
    [SerializeField] protected float _timeChangeState = 0.25f;
    //[SerializeField] protected ParticleSystem _attackEffect;
    [SerializeField] protected Transform attackPoint;

    public override void EnterState()
    {
        base.EnterState();
        //_attackEffect.Play();
    }

    public override void ExitState()
    {
        base.ExitState();
        _actionLayer.StartFade(0, 0.1f);
    }

    public virtual void Attack()
    {
        if (_blackboard.enemyController.player == null) return;

        var targetComponent = _blackboard.enemyController.player.GetComponent<IHitable>();

        var damage = _blackboard.enemyController.enemyData.RandomDamage(_blackboard.enemyController.enemyData.attackDamage.Value);
        targetComponent.OnHit(damage);
    }

}
