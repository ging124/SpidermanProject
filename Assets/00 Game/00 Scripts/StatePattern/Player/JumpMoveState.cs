
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneMoveState : CanMoveState
{
    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.character.Sprint();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if (_blackboard.inputSO.move != Vector2.zero)
        {
            Movement();
        }
    }

    public override void ExitState()
    {
        _blackboard.character.StopSprinting();
        base.ExitState();
    }
}
