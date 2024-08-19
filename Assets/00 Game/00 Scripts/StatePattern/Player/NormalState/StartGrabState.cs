using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGrabState : HitStatusState
{
    [SerializeField] private ClipTransition _startGrabAnim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.playerController.canAttack = false;
        _blackboard.character.SetMovementDirection(Vector3.zero);
        _normalBodyLayer.Play(_startGrabAnim).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.standUpState);
        };
        TakeDamage();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.playerController.hitAttackType == AttackType.NormalAttack)
        {
            _stateManager.ChangeState(_stateReferences.grabHitState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

}
