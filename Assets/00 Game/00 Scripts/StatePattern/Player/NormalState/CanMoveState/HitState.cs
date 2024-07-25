using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : CanMoveState
{
    [SerializeField] private ClipTransition _hitAnim;
    [SerializeField] private float _timeToIdle;

    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_hitAnim);
        TakeDamage();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Movement();

        if (!_blackboard.playerController.onHit && _elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleNormalState);
            return;
        }

        if (_blackboard.playerController.playerData.currentHP <= 0)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.deadState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public void TakeDamage()
    {
        int damageValue = _blackboard.playerController.hitDamage - _blackboard.playerController.playerData.armor;
        if(damageValue <= 0) damageValue = 1;
        _blackboard.playerController.playerData.currentHP -= damageValue;

        if (_blackboard.playerController.playerData.currentHP <= 0)
        {
            _blackboard.playerController.playerData.currentHP = 0;
        }

    }
}
