using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDownState : HitStatusState
{
    [SerializeField] private ClipTransition _knockDownAnim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.playerController.canAttack = false;
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

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.playerController.canAttack = true;
        base.ExitState();
    }

}
