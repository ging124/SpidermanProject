using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbMovementState : CanMoveState
{
    [SerializeField] private float _climbSpeed;
    [SerializeField] private LinearMixerTransition _climbBlendTree;
    [SerializeField] private float _timeToIdle;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.playerController.transform.DORotate(Quaternion.LookRotation(-_blackboard.frontWallHit.normal, Vector3.up).eulerAngles, 0.2f);
        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.isKinematic = false;
        _blackboard.playerController.rb.useGravity = false;
        _blackboard.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _normalBodyLayer.Play(_climbBlendTree);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        Movement();
        _climbBlendTree.State.Parameter = Mathf.Lerp(_climbBlendTree.State.Parameter, Vector3.Angle(_blackboard.playerController.transform.right, _blackboard.movement.normalized), 50 * Time.deltaTime);

        if (!_blackboard.wallInHead)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.exitClimbState);
            return;
        }

        if (_blackboard.movement == Vector3.zero && _blackboard.wallFront)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.climbState);
            return;
        }

        if (!_blackboard.wallFront && _elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
            return;
        }

        if (_blackboard.inputSO.buttonJump)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.startJumpHighState);
            return;
        }
    }

    public override void ExitState()
    {
        _blackboard.character.SetRotationMode(RotationMode.OrientToMovement);
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.rb.constraints = RigidbodyConstraints.None;
        base.ExitState();
    }

    protected override void Movement()
    {
        GetInput();

        _blackboard.rb.velocity = _blackboard.movement.normalized * _climbSpeed;
    }

    protected override void GetInput()
    {
        Vector2 input = _blackboard.inputSO.move;
        Vector3 vertical = _blackboard.playerController.transform.up * input.y;
        Vector3 horizontal = _blackboard.playerController.transform.right * input.x;
        _blackboard.movement = horizontal + vertical;
        RaycastHit hit;
        Physics.Raycast(_blackboard.playerController.transform.position, _blackboard.playerController.transform.forward, out hit);
        _blackboard.movement = Vector3.ProjectOnPlane(_blackboard.movement, hit.normal);
    }
}
