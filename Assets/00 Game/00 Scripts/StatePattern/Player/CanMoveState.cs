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
        _blackboard.character.SetMovementDirection(Vector3.zero);
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
        Vector3 vertical = _blackboard.cam.transform.forward * input.y;
        Vector3 horizontal = _blackboard.cam.transform.right * input.x;
        _blackboard.movement = (vertical + horizontal);
    }
}
