using UnityEngine;

public class EnemyAttackState : EnemyActionState
{
    //[SerializeField] protected ParticleSystem _attackEffect;
    [SerializeField] protected float _timeChangeState = 0.25f;


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

        if (!_blackboard.enemyController.canAttack && _elapsedTime > _timeChangeState
            || _stateManager.currentState == _stateReferences.enemyHitState
            || _stateManager.currentState == _stateReferences.enemyDeadState)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
            return StateStatus.Success;
        }

        if (_blackboard.enemyController.onHit)
        {
            _stateManager.ChangeState(_stateReferences.enemyHitState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
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
