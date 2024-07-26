using UnityEngine;

public class EnemyAttackState : EnemyActionState
{
    [SerializeField] protected float _timeChangeState = 0.25f;
    //[SerializeField] protected ParticleSystem _attackEffect;
    [SerializeField] protected Transform attackPoint;

    public override void EnterState(EnemyStateManager stateManager, EnemyBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        //_attackEffect.Play();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_blackboard.enemyController.canAttack && _elapsedTime > _timeChangeState
            || ((EnemyStateManagerAction)_stateManager).stateManagerMovement.currentState == _stateManager.stateReferences.enemyHitState
            || ((EnemyStateManagerAction)_stateManager).stateManagerMovement.currentState == _stateManager.stateReferences.enemyDeadState)
        {
            //_stateManager.ChangeState(_stateManager.stateReferences.enemyIdleActionState);
            return;
        }
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
            player.GetComponent<PlayerController>().OnHit(_blackboard.enemyController.enemyData.RandomDamage(_blackboard.enemyController.enemyData.attackDamage));
        }
    }
/*
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, _blackboard.enemyController.enemyData.attackRange);
    }*/
}
