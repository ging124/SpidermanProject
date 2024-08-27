using Animancer;
using UnityEngine;

public class FallState : OnAirState
{
    [SerializeField] private ClipTransition _onAirAnim;
    [SerializeField] private float _timeToDive = 0.5f;

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

        if (_blackboard.character.fallingTime > _timeToDive && !_blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateReferences.diveState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.buttonJump && !_blackboard.character.IsGrounded() && _elapsedTime > 0.5f && !Physics.Raycast(_blackboard.playerController.transform.position, -Vector3.up, 2))
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
