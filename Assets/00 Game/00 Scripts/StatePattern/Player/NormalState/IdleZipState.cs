using Animancer;
using UnityEngine;

public class IdleZipState : NormalState
{
    [SerializeField] private ClipTransition _idleZipAnim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.playerController.canZip = true;
        _normalBodyLayer.Play(_idleZipAnim);
        _blackboard.character.SetMovementDirection(Vector3.zero);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.inputSO.move != Vector2.zero)
        {
            _stateManager.ChangeState(_stateReferences.runState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.buttonJump)
        {
            _stateManager.ChangeState(_stateReferences.zipJumpState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
