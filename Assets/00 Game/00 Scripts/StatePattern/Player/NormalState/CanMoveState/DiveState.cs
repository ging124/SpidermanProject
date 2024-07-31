using Animancer;
using UnityEngine;

public class DiveState : AirborneMoveState
{
    [SerializeField] private ClipTransition _onAirAnim;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_onAirAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.character.IsGrounded() && _blackboard.character.GetVelocity().magnitude == 0 && _blackboard.inputSO.move == Vector2.zero)
        {
            _stateManager.ChangeState(_stateReferences.endJumpState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.character.GetVelocity().magnitude != 0)
        {
            _stateManager.ChangeState(_stateReferences.endJumpToRunState);
            return;
        }

        if (_blackboard.inputSO.buttonJump && !_blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateReferences.swingState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
