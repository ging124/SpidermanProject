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

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }


        if (!_blackboard.enemyController.canAttack && _elapsedTime > _timeChangeState
            || ((StateManagerAction)_stateManager).stateManagerMovement.currentState == _stateReferences.enemyHitState
            || ((StateManagerAction)_stateManager).stateManagerMovement.currentState == _stateReferences.enemyDeadState)
        {
            //_stateManager.ChangeState(_stateManager.stateReferences.enemyIdleActionState);
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
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.transform.position, _blackboard.enemyController.enemyData.attackRange, _blackboard.enemyController.enemyData.playerLayer);
        foreach (Collider player in hitPlayer)
        {
            player.GetComponent<PlayerController>().OnHit(_blackboard.enemyController.enemyData.RandomDamage(_blackboard.enemyController.enemyData.attackDamage.Value));
        }
    }
/*
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, _blackboard.enemyController.enemyData.attackRange);
    }*/
}
