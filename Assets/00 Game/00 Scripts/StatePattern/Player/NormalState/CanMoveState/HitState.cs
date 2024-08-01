using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : CanMoveState
{
    [SerializeField] private ClipTransition _hitAnim;
    [SerializeField] private float _timeToIdle;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_hitAnim);
        TakeDamage();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        Movement();

        if (!_blackboard.playerController.onHit && _elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateReferences.idleNormalState);
            return StateStatus.Success;
        }

        if (_blackboard.playerController.playerData.currentHP <= 0)
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
        if(damageValue <= 0) damageValue = 1;
        _blackboard.playerController.playerData.currentHP -= damageValue;

        if (_blackboard.playerController.playerData.currentHP <= 0)
        {
            _blackboard.playerController.playerData.currentHP = 0;
        }

    }
}
