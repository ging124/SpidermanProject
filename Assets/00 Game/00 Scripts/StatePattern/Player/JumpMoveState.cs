
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneMoveState : CanMoveState
{
    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.inputSO.move != Vector2.zero)
        {
            _blackboard.character.StopSprinting();
            Movement();
        }

        if (_blackboard.inputSO.buttonRun)
        {
            _blackboard.character.Sprint();
            Movement();
        }
    }

    public override void ExitState()
    {
        _blackboard.character.StopSprinting();
        base.ExitState();
    }
}
