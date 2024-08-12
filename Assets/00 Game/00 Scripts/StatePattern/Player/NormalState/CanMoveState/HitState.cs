using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitState : CanMoveState
{
    [SerializeField] private ClipTransition _hitAnim;
    [SerializeField] private float _timeToIdle;

    public override void EnterState()
    {
        base.EnterState();
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
        _blackboard.playerController.playerData.currentHP -= damageValue;
        _blackboard.playerController.playerChangeHP.Invoke(_blackboard.playerController.playerData.currentHP, _blackboard.playerController.playerData.maxHP.Value);

        if (_blackboard.playerController.playerData.currentHP <= 0)
        {
            _blackboard.playerController.playerData.currentHP = 0;
        }
    }
}
