public class MovementState : CanMoveState
{
    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
            return;
        }

        if (_blackboard.playerController.wallFront)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.climbState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
