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

        if (_stateManager.currentState != _stateReferences.dodgeState && _blackboard.inputSO.buttonDodge)
        {
            _stateManager.ChangeState(_stateReferences.dodgeState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.buttonJump && _blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateReferences.jumpState);
            return StateStatus.Success;
        }

        if (!_blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateReferences.fallState);
            return StateStatus.Success;
        }

        if (_blackboard.playerController.wallFront)
        {
            _stateManager.ChangeState(_stateReferences.climbState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()    
    {
        base.ExitState();
    }

    
}
