using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHitState : HitStatusState
{
    [SerializeField] private ClipTransition _grabHitAnim;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_grabHitAnim, 0.25f, FadeMode.FromStart);
        TakeDamage();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.playerController.hitAttackType == AttackType.NormalAttack && _elapsedTime > 0.1f)
        {
            _stateManager.ChangeState(_stateReferences.grabHitState);
            return StateStatus.Success;
        }

        if (_blackboard.playerController.hitAttackType == AttackType.EndGrabAttack)
        {
            _stateManager.ChangeState(_stateReferences.endGrabState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
