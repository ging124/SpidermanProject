using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : ActionState
{
    [SerializeField] protected float _timeChangeState = 0.25f;
    [SerializeField] protected ParticleSystem _attackEffect;
    [SerializeField] protected float attackRange = 1.0f;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected Transform attackPoint;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _attackEffect.Play();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_blackboard.inputSO.buttonAttack && _elapsedTime > _timeChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleActionState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        _actionLayer.StartFade(0, 0.1f);
    }

    public virtual void Attack()
    {
        Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.transform.position, attackRange, enemyLayer);
        foreach (Collider enemy in hitEnemy)
        {
            //enemy.GetComponent<EnemyController>().OnHit(_blackboard.playerData.RandomDamage(_blackboard.playerController.attackDamage));
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }

}
