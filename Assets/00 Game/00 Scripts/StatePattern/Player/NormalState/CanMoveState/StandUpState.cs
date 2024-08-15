using Animancer;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUpState : NormalState
{
    [SerializeField] private ClipTransition _standUpAnim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.playerController.canAttack = false;
        _normalBodyLayer.Play(_standUpAnim).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.idleNormalState);
        };
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
        _blackboard.character.useRootMotion = false;
        _blackboard.playerController.canHit = true;
        _blackboard.playerController.canAttack = true;
        base.ExitState();
    }
}
