using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

public class ClimbState : CanMoveState
{
    [SerializeField] private ClipTransition _idleClimbAnim;
    [SerializeField] private float _timeToChangeState;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.playerController.transform.DORotate(Quaternion.LookRotation(_blackboard.playerController.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles, 0.2f);
        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.isKinematic = false;
        _blackboard.playerController.rb.useGravity = false;
        _normalBodyLayer.Play(_idleClimbAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        if (!_blackboard.wallFront)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.exitClimbState);
            return;
        }

        if(_blackboard.inputSO.buttonJump && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.climbJumpState);
            return;
        }

        if (_blackboard.inputSO.move != Vector2.zero && _blackboard.inputSO.move.y >= 0 && _blackboard.wallFront)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.climbMovementState);
            return;
        }
    }

    public override void ExitState()
    {
        Vector3 velocity = _blackboard.rb.velocity;
        _blackboard.character.SetRotationMode(RotationMode.OrientToMovement);
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.character.SetVelocity(velocity);
        base.ExitState();
    }
}
