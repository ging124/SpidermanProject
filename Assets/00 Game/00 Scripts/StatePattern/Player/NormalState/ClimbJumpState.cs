using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbJumpState : NormalState
{
    [SerializeField] private ClipTransition _climbJumpAnim;
    [SerializeField] private float _timeToChangeState;
    [SerializeField] private float _climbForce;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.isKinematic = false;
        _blackboard.playerController.rb.useGravity = false;
        _normalBodyLayer.Play(_climbJumpAnim);
        _blackboard.playerController.rb.velocity = _blackboard.playerController.frontWallHit.normal * _climbForce;
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateReferences.fallState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        Vector3 velocity = _blackboard.playerController.rb.velocity;
        _blackboard.character.SetRotationMode(RotationMode.OrientToMovement);
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.character.SetVelocity(velocity);
        base.ExitState();
    }
}
