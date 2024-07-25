using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using System;
using UnityEngine;

public class StartZipState : NormalState
{
    [SerializeField] private ClipTransition _zipAnim;
    [SerializeField] private float _zipTime;
    [SerializeField] private float _zipSpeed;
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;
    [SerializeField] private Vector3 zipPoint;


    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_zipAnim);
        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.useGravity = true;
        _blackboard.playerController.rb.isKinematic = false;

        zipPoint = _blackboard.playerController.zipPoint;

        _zipTime = Vector3.Distance(_blackboard.playerController.transform.position, zipPoint) / _zipSpeed;

        if(_zipTime < 0.4)
        {
            _zipTime = 0.4f;
        }

        StartZip();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        if (_elapsedTime > _zipTime)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleZipState);
        }

        if (_blackboard.inputSO.buttonJump && _elapsedTime > _zipTime - _zipTime * 0.25f)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.zipJumpState);
        }
    }

    public override void ExitState()
    {
        _leftLineRenderer.positionCount = 0;
        _rightLineRenderer.positionCount = 0;
        Vector3 velocity = _blackboard.playerController.rb.velocity;
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        _blackboard.playerController.rb.useGravity = false;
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.character.SetVelocity(velocity);
        base.ExitState();
    }

    public void Zip()
    {
        _blackboard.transform.DOLookAt(zipPoint, 0.2f, AxisConstraint.Y);
        _blackboard.transform.DOMove(zipPoint + Vector3.up, _zipTime);
    }

    public void StartZip()
    {
        _leftLineRenderer.positionCount = 2;
        _rightLineRenderer.positionCount = 2;

        SetLineRenderer(_leftLineRenderer, _blackboard.playerController.leftHand);
        SetLineRenderer(_rightLineRenderer, _blackboard.playerController.rightHand);
    }

    private void SetLineRenderer(LineRenderer lineRenderer, Transform hand)
    {
        lineRenderer.SetPosition(0, hand.position);
        lineRenderer.SetPosition(1, zipPoint);
    }
}
