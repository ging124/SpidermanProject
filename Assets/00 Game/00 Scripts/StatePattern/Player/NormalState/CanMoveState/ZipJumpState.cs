using Animancer;
using EasyCharacterMovement;
using UnityEngine;

public class ZipJumpState : OnAirState
{
    [SerializeField] private ClipTransition _ZipJumpAnim;
    [SerializeField] private float _timeToChangeState;
    [SerializeField] private float _jumpForce;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_ZipJumpAnim);
        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.useGravity = true;
        _blackboard.playerController.rb.isKinematic = false;
        _blackboard.playerController.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        _blackboard.playerController.rb.AddForce((_blackboard.playerController.transform.up * 150) * _jumpForce);
        _blackboard.playerController.rb.AddForce((_blackboard.playerController.transform.forward * 175) * _jumpForce);

    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (!_blackboard.character.IsGrounded() && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateReferences.fallState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        Vector3 velocity = _blackboard.playerController.rb.velocity;
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        _blackboard.playerController.rb.useGravity = false;
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.playerController.rb.constraints = RigidbodyConstraints.None;
        _blackboard.character.SetVelocity(velocity);
        base.ExitState();
    }
}
