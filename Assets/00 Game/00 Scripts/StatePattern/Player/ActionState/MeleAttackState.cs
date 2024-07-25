using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttackState : AttackState
{
    [SerializeField] private List<Combo> _listCombo;
    [SerializeField] private int combo = -1;
    [SerializeField] private int hit = 0;
    [SerializeField] protected float attackRange = 1.0f;

    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.character.useRootMotion = true;

        if (combo == -1)
        {
            combo = Random.Range(0, _listCombo.Count);
        }
        else
        {
            hit++;
        }

        _actionLayer.Play(_listCombo[combo].hitList[hit].hitAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_blackboard.inputSO.buttonAttack && _elapsedTime > _listCombo[combo].hitList[hit].timeEndAttack)
        {
            ResetCombo();
            _stateManager.ChangeState(_stateManager.stateReferences.idleActionState);
        }

        if (_blackboard.inputSO.buttonAttack && _elapsedTime > _listCombo[combo].hitList[hit].timeNextAttack)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.meleAttackState);
        }
    }

    public override void ExitState()
    {
        LoopCombo();
        _blackboard.character.useRootMotion = false;
        base.ExitState();
    }

    public void LoopCombo()
    {
        if (hit >= 3)
        {
            combo = -1;
            hit = 0;
        }
    }

    public void ResetCombo()
    {
        hit = 0;
        combo = -1;
    }

    /*public virtual void Attack()
    {
        Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.transform.position, attackRange, _blackboard.playerController.enemyLayer);
        foreach (Collider enemy in hitEnemy)
        {
            enemy.GetComponent<EnemyController>().OnHit(_blackboard.playerData.RandomDamage(_blackboard.playerController.attackDamage));
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }*/
}
