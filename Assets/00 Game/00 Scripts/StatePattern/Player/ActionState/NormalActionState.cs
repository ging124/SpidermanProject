using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalActionState : ActionState
{
    [SerializeField] private float _timeToAttack = 0.15f;

    public override void EnterState()
    {
        base.EnterState();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if(_blackboard.inputSO.buttonAttack && _elapsedTime > _timeToAttack
            && _blackboard.character.IsGrounded()
            && ((StateManagerAction)_stateManager).stateManagerMovement.currentState != _stateReferences.deadState)
        {
            if (_blackboard.playerController.enemyTarget == null)
            {
                _stateManager.ChangeState(_stateReferences.meleAttackState);
                return StateStatus.Success;
            }
            else
            {
                Vector3 player = _blackboard.playerController.transform.position;
                Vector3 target = _blackboard.playerController.enemyTarget.transform.position;
                float distance = Vector3.Distance(player, target);
                if (distance >= _blackboard.playerController.farAttackRange)
                {
                    _stateManager.ChangeState(_stateReferences.farAttackState);
                    return StateStatus.Success;
                }
                else if (distance >= _blackboard.playerController.mediumAttackRange && distance < _blackboard.playerController.farAttackRange)
                {
                    _stateManager.ChangeState(_stateReferences.mediumAttackState);
                    return StateStatus.Success;
                }
                else if (distance >= _blackboard.playerController.nearAttackRange && distance < _blackboard.playerController.mediumAttackRange)
                {
                    _stateManager.ChangeState(_stateReferences.nearAttackState);
                    return StateStatus.Success;
                }
                else
                {
                    _stateManager.ChangeState(_stateReferences.meleAttackState);
                    return StateStatus.Success;
                }
            }
        }

        if(_blackboard.inputSO.buttonGadget 
            && _blackboard.character.IsGrounded()
            && ((StateManagerAction)_stateManager).stateManagerMovement.currentState != _stateReferences.deadState)
        {
            _stateManager.ChangeState(_stateReferences.webShooterState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.buttonUltimate
            && _blackboard.character.IsGrounded()
            && ((StateManagerAction)_stateManager).stateManagerMovement.currentState != _stateReferences.deadState)
        {
            _stateManager.ChangeState(_stateReferences.ultimateAttackState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
