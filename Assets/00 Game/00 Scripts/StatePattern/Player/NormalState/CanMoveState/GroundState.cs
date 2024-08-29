using UnityEngine;

public class GroundState : NormalState
{
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

        if (_stateManager.currentState != _stateReferences.dodgeState 
            && _blackboard.inputSO.buttonDodge)
        {
            _stateManager.ChangeState(_stateReferences.dodgeState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.buttonJump 
            && _blackboard.character.IsGrounded())
            //&& _stateManager.currentState != _stateReferences.useGadgetState)
        {
            _stateManager.ChangeState(_stateReferences.jumpState);
            return StateStatus.Success;
        }

        if (!_blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateReferences.fallState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.buttonAttack 
            && _blackboard.character.IsGrounded()
            && _blackboard.playerController.canAttack)
        {
            if (_blackboard.playerController.target == null)
            {
                _stateManager.ChangeState(_stateReferences.meleAttackState);
                return StateStatus.Success;
            }
            else
            {
                Vector3 player = _blackboard.playerController.transform.position;
                Vector3 target = _blackboard.playerController.target.transform.position;
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
                else if (distance < _blackboard.playerController.nearAttackRange)
                {
                    _stateManager.ChangeState(_stateReferences.meleAttackState);
                    return StateStatus.Success;
                }
            }
        }

        if(_blackboard.inputSO.buttonUltimate)
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
