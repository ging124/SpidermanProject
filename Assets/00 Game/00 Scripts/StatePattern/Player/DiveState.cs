using Animancer;
using UnityEngine;

public class DiveState : AirborneMoveState
{
    [SerializeField] private ClipTransition _onAirAnim;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_onAirAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.character.IsGrounded() && _blackboard.inputSO.move == Vector2.zero)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpState);
            return;
        }

        if (_blackboard.character.IsGrounded() && !_blackboard.inputSO.buttonRun)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpToWalkState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.inputSO.buttonRun)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpToRunState);
            return;
        }

        if (_blackboard.inputSO.buttonSwing)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.swingState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
