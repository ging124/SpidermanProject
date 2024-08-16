using Animancer;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class EndGrabState : HitStatusState
{
    [SerializeField] private ClipTransition _endGrabAnim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.character.useRootMotion = true;
        TakeDamage();
        _normalBodyLayer.Play(_endGrabAnim).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.standUpState);
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
        _blackboard.playerController.canAttack = true;
        base.ExitState();
    }
}
