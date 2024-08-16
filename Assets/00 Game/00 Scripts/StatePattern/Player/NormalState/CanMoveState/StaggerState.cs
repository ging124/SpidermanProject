using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaggerState : HitStatusState
{
    [SerializeField] private ClipTransition _hitAnim;
    [SerializeField] private float _timeToIdle;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.playerController.canAttack = false;
        _normalBodyLayer.Play(_hitAnim, 0.25f, FadeMode.FromStart);
        TakeDamage();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.playerController.hitAttackType == AttackType.None && _elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateReferences.idleNormalState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.playerController.canAttack = true;
        base.ExitState();
    }

    
}
