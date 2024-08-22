using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using System;
using System.Collections;
using UnityEngine;

public class StartZipState : NormalState
{
    [SerializeField] private ClipTransition _zipAnim;
    [SerializeField] private float _zipTime;
    [SerializeField] private float _zipSpeed;
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;
    [SerializeField] private Vector3 zipPoint;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_zipAnim);
        _blackboard.playerController.canZip = false;
        _blackboard.playerController.rb.interpolation = RigidbodyInterpolation.None;
        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.isKinematic = false;

        zipPoint = _blackboard.playerController.zipPoint;

        _zipTime = Vector3.Distance(_blackboard.playerController.transform.position, zipPoint) / _zipSpeed;

        if(_zipTime < 0.4)
        {
            _zipTime = 0.4f;
        }

        StartZip();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_elapsedTime > _zipTime)
        {
            _stateManager.ChangeState(_stateReferences.idleZipState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.buttonJump && _elapsedTime > _zipTime - _zipTime * 0.25f)
        {
            _stateManager.ChangeState(_stateReferences.zipJumpState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _leftLineRenderer.positionCount = 0;
        _rightLineRenderer.positionCount = 0;
        _blackboard.playerController.rb.interpolation = RigidbodyInterpolation.Interpolate;
        Vector3 velocity = _blackboard.playerController.rb.velocity;
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.character.SetVelocity(velocity);
        base.ExitState();
    }

    public void Zip()
    {
        _blackboard.transform.DOLookAt(zipPoint, 0.2f, AxisConstraint.Y);
        _blackboard.playerController.transform.DOMove(zipPoint + Vector3.up, _zipTime).OnComplete(() =>
        {
            _blackboard.playerController.rb.interpolation = RigidbodyInterpolation.Interpolate;
        });
    }

    public void StartZip()
    {
        _leftLineRenderer.positionCount = 2;
        _rightLineRenderer.positionCount = 2;

        _blackboard.playerController.SetLineRenderer(_leftLineRenderer, _blackboard.playerModel.leftHand, zipPoint);
        _blackboard.playerController.SetLineRenderer(_rightLineRenderer, _blackboard.playerModel.rightHand, zipPoint);
    }
}
