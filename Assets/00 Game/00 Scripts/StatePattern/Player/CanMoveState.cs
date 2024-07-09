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

    protected virtual void Movement(float speed)
    {
        GetInput();

        _blackboard.charController.Move(new Vector3(_blackboard.movement.x, 0, _blackboard.movement.z) * speed * Time.deltaTime);

        if (_blackboard.movement != Vector3.zero)
        {
            _blackboard.transform.forward = Vector3.Lerp(_blackboard.transform.forward, new Vector3(_blackboard.movement.x, 0, _blackboard.movement.z), _blackboard.playerData.rotateSpeed * Time.deltaTime);
        }
    }

    protected virtual void GetInput()
    {
        Vector2 input = _blackboard.inputSO.move;
        Vector3 horizontal = _blackboard.cam.transform.right * input.x;
        Vector3 vertical = _blackboard.cam.transform.forward * input.y;
        _blackboard.movement = (vertical + horizontal).normalized;
    }
}
