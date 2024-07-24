using Animancer;
using EasyCharacterMovement;
using UnityEngine;

public class ZipJumpState : AirborneMoveState
{
    [SerializeField] private ClipTransition _ZipJumpAnim;
    [SerializeField] private float _timeToChangeState;
    [SerializeField] private float _jumpForce;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_ZipJumpAnim);
        Vector3 velocity = _blackboard.character.GetVelocity();
        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.useGravity = true;
        _blackboard.playerController.rb.isKinematic = false;
        _blackboard.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _blackboard.rb.velocity = velocity;

        _blackboard.rb.AddForce((_blackboard.playerController.transform.up * 75) * _jumpForce);
        _blackboard.rb.AddForce((_blackboard.playerController.transform.forward * 100) * _jumpForce);

    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        if (!_blackboard.character.IsGrounded() && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleNormalState);
            return;
        }
    }

    public override void ExitState()
    {
        Vector3 velocity = _blackboard.rb.velocity;
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        _blackboard.playerController.rb.useGravity = false;
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.rb.constraints = RigidbodyConstraints.None;
        _blackboard.character.SetVelocity(velocity);
        base.ExitState();
    }
}
