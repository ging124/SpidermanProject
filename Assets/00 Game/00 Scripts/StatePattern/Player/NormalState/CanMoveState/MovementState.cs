public class MovementState : CanMoveState
{
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateReferences.onAirState);
            return;
        }

        if (_blackboard.playerController.wallFront)
        {
            _stateManager.ChangeState(_stateReferences.climbState);
            return;
        }
    }

    public override void ExitState()    
    {
        base.ExitState();
    }
}
