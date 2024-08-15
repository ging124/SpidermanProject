using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDownState : NormalState
{
    [SerializeField] private ClipTransition _knockDownAnim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.character.useRootMotion = true;
        _normalBodyLayer.Play(_knockDownAnim).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.standUpState);
        };
        TakeDamage();
        _blackboard.playerController.canHit = false;
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.playerController.currentHP <= 0)
        {
            _stateManager.ChangeState(_stateReferences.deadState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public void TakeDamage()
    {
        int damageValue = _blackboard.playerController.hitDamage;
        _blackboard.playerController.currentHP -= damageValue;
        _blackboard.playerController.playerChangeHP.Invoke(_blackboard.playerController.currentHP, _blackboard.playerController.playerData.maxHP);

        if (_blackboard.playerController.currentHP <= 0)
        {
            _blackboard.playerController.currentHP = 0;
        }
    }
}
