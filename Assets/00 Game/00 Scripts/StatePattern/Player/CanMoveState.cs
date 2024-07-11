using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMoveState : NormalState
{
    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    protected virtual void Movement()
    {
        GetInput();

        _blackboard.character.SetMovementDirection(_blackboard.movement);
    }

    protected virtual void GetInput()
    {
        Vector2 input = _blackboard.inputSO.move;
        Vector3 horizontal = _blackboard.cam.transform.right * input.x;
        Vector3 vertical = _blackboard.cam.transform.forward * input.y;
        _blackboard.movement = (vertical + horizontal);
    }
}
