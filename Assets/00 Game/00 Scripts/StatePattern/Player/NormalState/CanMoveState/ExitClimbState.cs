using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitClimbState : CanMoveState
{
    [SerializeField] private float _climbSpeed;
    [SerializeField] private LinearMixerTransition _exitClimbAnim;
    [SerializeField] private float _timeToChangeState = 0.25f;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.isKinematic = false;
        _blackboard.playerController.rb.useGravity = false;
        _blackboard.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _normalBodyLayer.Play(_exitClimbAnim);
    }
        
    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        Movement();
        _exitClimbAnim.State.Parameter = Mathf.Lerp(_exitClimbAnim.State.Parameter, Vector3.Angle(_blackboard.playerController.transform.right, _blackboard.movement.normalized), 40 * Time.deltaTime);

        if (_blackboard.wallFront)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.climbState);
            return;
        }

        if (!_blackboard.wallFront && _blackboard.inputSO.buttonJump && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.jumpState);
            return;
        }

        if (!_blackboard.wallFront && _blackboard.character.IsGrounded() && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
            return;
        }

        if (!_blackboard.wallFront && _blackboard.inputSO.move == Vector2.zero && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.stopRunState);
            return;
        }

        if (!_blackboard.wallFront && _blackboard.inputSO.move != Vector2.zero && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.runState);
            return;
        }
    }

    public override void ExitState()
    {
        Vector3 velocity = _blackboard.rb.velocity;
        _blackboard.character.SetRotationMode(RotationMode.OrientToMovement);
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.rb.constraints = RigidbodyConstraints.None;
        _blackboard.character.SetVelocity(velocity + _blackboard.playerController.transform.forward * 10);
        base.ExitState();
    }

    protected override void Movement()
    {
        GetInput();

        _blackboard.rb.velocity = _blackboard.movement.normalized * _climbSpeed * Time.deltaTime * 20;
    }

    protected override void GetInput()
    {
        Vector2 input = _blackboard.inputSO.move;

        if (input.y < 0)
        {
            input = new Vector2(1, 0);
        }

        Vector3 vertical = _blackboard.playerController.transform.up * input.y;
        Vector3 horizontal = _blackboard.playerController.transform.right * input.x;
        _blackboard.movement = horizontal + vertical;
        _blackboard.movement = Vector3.ProjectOnPlane(_blackboard.movement, _blackboard.frontWallHit.normal);
    }
}
