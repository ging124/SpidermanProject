using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAttackState : ActionState
{
    [SerializeField] private ClipTransition _webShooterAnim;
    [SerializeField] private float _timeToAttack = 0.15f;

    public override void EnterState()
    {
        base.EnterState();
        _actionLayer.Play(_webShooterAnim).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.normalActionState);
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
        _actionLayer.StartFade(0, 0.1f);
        base.ExitState();
    }
}
