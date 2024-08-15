using UnityEngine;

public class OnAirState : NormalState
{
    public override void EnterState()
    {
        base.EnterState();
        _blackboard.playerController.canAttack = false;
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        Movement();

        if (_blackboard.playerController.wallFront)
        {
            _stateManager.ChangeState(_stateReferences.climbState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.character.GetVelocity().magnitude == 0 && _blackboard.inputSO.move == Vector2.zero && _elapsedTime > 0.25f)
        {
            _stateManager.ChangeState(_stateReferences.landState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.character.GetVelocity().magnitude < 5 && _elapsedTime > 0.25f)
        {
            _stateManager.ChangeState(_stateReferences.landLowState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.character.GetVelocity().magnitude >= 5 && _elapsedTime > 0.25f)
        {
            _stateManager.ChangeState(_stateReferences.landHighState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        _blackboard.playerController.canAttack = true;
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
