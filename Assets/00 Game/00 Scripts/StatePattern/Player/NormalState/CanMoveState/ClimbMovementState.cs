using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

public class ClimbMovementState : NormalState
{
    [SerializeField] private float _climbSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float _timeToChangeState;
    [SerializeField] private LinearMixerTransition _climbBlendTree;
    [SerializeField] private ClipTransition _idleClimbAnim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.isKinematic = false;
        _blackboard.playerController.rb.useGravity = false;
        _normalBodyLayer.Play(_climbBlendTree);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        SetAnimancer();

        if (!_blackboard.playerController.wallFront)
        {
            _stateManager.ChangeState(_stateReferences.exitClimbState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.buttonJump && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateReferences.climbJumpState);
            return StateStatus.Success;
        }

        if (_blackboard.playerController.movement == Vector3.zero && _blackboard.playerController.wallFront)
        {
            _stateManager.ChangeState(_stateReferences.climbState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void FixedUpdateState()
    {
        Rotation();
        Movement();
    }

    public override void ExitState()
    {
        Vector3 velocity = _blackboard.playerController.rb.velocity;
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.character.SetVelocity(velocity);
        base.ExitState();
    }

    protected void Movement()
    {
        GetInput();

        _blackboard.playerController.rb.velocity = _blackboard.playerController.movement.normalized * _climbSpeed * Time.fixedDeltaTime * 20;
    }

    protected void GetInput()
    {
        Vector2 input = _blackboard.inputSO.move;

        if (input.y < 0) input = Vector2.zero;

        Vector3 vertical = _blackboard.playerController.transform.up * input.y;
        Vector3 horizontal = _blackboard.playerController.transform.right * input.x;
        _blackboard.playerController.movement = horizontal + vertical;
        _blackboard.playerController.movement = Vector3.ProjectOnPlane(_blackboard.playerController.movement, _blackboard.playerController.frontWallHit.normal);
    }

    public void SetAnimancer()
    {
        if (_blackboard.inputSO.move == Vector2.zero)
        {
            _normalBodyLayer.Play(_idleClimbAnim);
        }
        else
        {
            _climbBlendTree.State.Parameter = Mathf.Lerp(_climbBlendTree.State.Parameter, Vector3.Angle(_blackboard.playerController.transform.right, _blackboard.playerController.movement.normalized), 60 * Time.deltaTime);
        }
    }

    public void Rotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(-_blackboard.playerController.frontWallHit.normal, Vector3.up);

        _blackboard.playerController.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}

