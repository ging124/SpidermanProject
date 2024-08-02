using Animancer;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleAttackState : AttackState
{
    [SerializeField] private List<Combo> listCombo;
    [SerializeField] private int combo = -1;
    [SerializeField] private int hit = 0;

    public override void EnterState()
    {
        base.EnterState();

        CountCombo();
        _actionLayer.Play(listCombo[combo].hitList[hit].hitAnim).Events.OnEnd = () =>
        {
            ResetCombo();
            _stateManager.ChangeState(_stateReferences.idleActionState);
        };
        listCombo[combo].hitList[hit].hitAnim.Events.SetCallback("Attack", Attack);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.inputSO.buttonAttack && _elapsedTime > listCombo[combo].hitList[hit].timeNextAttack && _blackboard.playerController.enemyTarget == null)
        {
            _stateManager.ChangeState(_stateReferences.meleAttackState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.buttonAttack && _blackboard.playerController.enemyTarget != null && _elapsedTime > listCombo[combo].hitList[hit].timeNextAttack)
        {
            Vector3 player = _blackboard.playerController.transform.position;
            Vector3 target = _blackboard.playerController.enemyTarget.transform.position;
            float distance = Vector3.Distance(player, target);
            if (distance >= _blackboard.playerController.farAttackRange)
            {
                _stateManager.ChangeState(_stateReferences.farAttackState);
                return StateStatus.Success;
            }
            else if (distance >= _blackboard.playerController.mediumAttackRange && distance < _blackboard.playerController.farAttackRange)
            {
                _stateManager.ChangeState(_stateReferences.mediumAttackState);
                return StateStatus.Success;
            }
            else if (distance >= _blackboard.playerController.nearAttackRange && distance < _blackboard.playerController.mediumAttackRange)
            {
                _stateManager.ChangeState(_stateReferences.nearAttackState);
                return StateStatus.Success;
            }
            else
            {
                _stateManager.ChangeState(_stateReferences.meleAttackState);
                return StateStatus.Success;
            }
        }

        return StateStatus.Running;
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
}
