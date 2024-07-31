using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalActionState : ActionState
{
    public float _timeToAttack = 0.15f;

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.inputSO.buttonAttack
            && _blackboard.character.IsGrounded()
            && ((StateManagerAction)_stateManager).stateManagerMovement.currentState != _stateReferences.deadState 
            && _elapsedTime > _timeToAttack)
        {
            _stateManager.ChangeState(_stateReferences.firstAttackState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
