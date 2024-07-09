using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleActionState : ActionState
{
    public float _timeToAttack = 0.15f;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.inputSO.buttonAttack
            && ((StateManagerAction)_stateManager).stateManagerMovement.currentState != _stateManager.stateReferences.deadState
            && ((StateManagerAction)_stateManager).stateManagerMovement.currentState != _stateManager.stateReferences.rollState 
            && !_blackboard.inputSO.buttonHolding && _elapsedTime > _timeToAttack)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.attack1State);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
