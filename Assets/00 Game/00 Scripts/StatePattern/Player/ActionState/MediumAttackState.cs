using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumAttackState : AttackState
{
    [SerializeField] private Hit mediumHit;

    public override void EnterState()
    {
        base.EnterState();

        _normalBodyLayer.Play(mediumHit.hitAnim).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.idleActionState);
        };
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.inputSO.buttonAttack && _elapsedTime > mediumHit.timeNextAttack)
        {
            _stateManager.ChangeState(_stateReferences.meleAttackState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public void MediumAttack()
    {
        Attack(mediumHit.attackType);
    }
}
