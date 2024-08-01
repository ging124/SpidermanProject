using Animancer;
using UnityEngine;

public class DiveState : OnAirState
{
    [SerializeField] private ClipTransition _onAirAnim;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_onAirAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.inputSO.buttonJump && !_blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateReferences.swingState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
