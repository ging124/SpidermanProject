using Animancer;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttackState : AttackState
{
    [SerializeField] private List<Combo> listCombo;
    [SerializeField] private int combo = -1;
    [SerializeField] private int hit = 0;

    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);

        CountCombo();
        _actionLayer.Play(listCombo[combo].hitList[hit].hitAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_blackboard.inputSO.buttonAttack && _elapsedTime > listCombo[combo].hitList[hit].timeEndAttack)
        {
            ResetCombo();
            _stateManager.ChangeState(_stateManager.stateReferences.idleActionState);
        }

        if (_blackboard.inputSO.buttonAttack && hit != listCombo[combo].hitList.Count - 1 && _elapsedTime > listCombo[combo].hitList[hit].timeNextAttack)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.meleAttackState);
        }

        if (_blackboard.inputSO.buttonAttack && hit == listCombo[combo].hitList.Count - 1  && _elapsedTime > listCombo[combo].hitList[hit].timeNextAttack
            || _blackboard.inputSO.buttonAttack && EnemyTargetDistance() >= _blackboard.playerController.mediumAttackRange && _elapsedTime > listCombo[combo].hitList[hit].timeNextAttack)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.firstAttackState);
        }
    }

    public override void ExitState()
    {
        LoopCombo();
        base.ExitState();
    }

    public void CountCombo()
    {
        if (combo == -1)
        {
            combo = Random.Range(0, listCombo.Count);
        }
        else
        {
            hit++;
        }
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

    public float EnemyTargetDistance()
    {
        if (_blackboard.playerController.enemyTarget == null)
        {
            return 0;
        }
        else
        {
            return Vector3.Distance(_blackboard.playerController.enemyTarget.transform.position, _blackboard.playerController.transform.position);
        }
    }
}
