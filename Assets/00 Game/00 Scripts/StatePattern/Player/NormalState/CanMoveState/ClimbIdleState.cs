using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

public class ClimbIdleState : NormalState
{
    [SerializeField] private ClipTransition _idleClimbAnim;
    [SerializeField] private float _timeToChangeState;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.playerController.canAttack = false;
        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.isKinematic = false;
        _blackboard.playerController.rb.useGravity = false;
        _blackboard.playerController.transform.DORotate(Quaternion.LookRotation(-_blackboard.playerController.frontWallHit.normal, Vector3.up).eulerAngles, 0.2f);
        _normalBodyLayer.Play(_idleClimbAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (!_blackboard.playerController.wallFront)
        {
            _stateManager.ChangeState(_stateReferences.exitClimbState);
            return StateStatus.Success;
        }

        if(_blackboard.inputSO.buttonJump && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateReferences.climbJumpState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.move != Vector2.zero && _blackboard.inputSO.move.y >= 0 && _blackboard.playerController.wallFront)
        {
            _stateManager.ChangeState(_stateReferences.climbMovementState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        Vector3 velocity = _blackboard.playerController.rb.velocity;
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.character.SetVelocity(velocity);
        base.ExitState();
    }
}
