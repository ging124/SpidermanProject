using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMoveState : NormalState
{
    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
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

        _blackboard.character.SetMovementDirection(_blackboard.playerController.movement.normalized);
    }

    protected virtual void GetInput()
    {
        Vector2 input = _blackboard.inputSO.move;
        Vector3 vertical = _blackboard.playerController.cam.transform.forward * input.y;
        Vector3 horizontal = _blackboard.playerController.cam.transform.right * input.x;
        _blackboard.playerController.movement = (vertical + horizontal);
        _blackboard.playerController.movement.y = 0;
    }
}
