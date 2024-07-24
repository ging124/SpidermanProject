using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

public class ClimbMovementState : CanMoveState
{
    [SerializeField] private float _climbSpeed;
    [SerializeField] private float _timeToChangeState;
    [SerializeField] private LinearMixerTransition _climbBlendTree;
    [SerializeField] private ClipTransition _idleClimbAnim;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.playerController.transform.DORotate(Quaternion.LookRotation(-_blackboard.frontWallHit.normal, Vector3.up).eulerAngles, 0.2f);
        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.isKinematic = false;
        _blackboard.playerController.rb.useGravity = false;
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
        SetAnimancer();

        if (!_blackboard.wallFront)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.exitClimbState);
            return;
        }

        if (_blackboard.inputSO.buttonJump && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.climbJumpState);
            return;
        }

        if (_blackboard.movement == Vector3.zero && _blackboard.wallFront)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.climbState);
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

    protected override void Movement()
    {
        GetInput();

        _blackboard.rb.velocity = _blackboard.movement.normalized * _climbSpeed * Time.deltaTime * 20;
    }

    protected override void GetInput()
    {
        Vector2 input = _blackboard.inputSO.move;

        if (input.y < 0) input = Vector2.zero;

        Vector3 vertical = _blackboard.playerController.transform.up * input.y;
        Vector3 horizontal = _blackboard.playerController.transform.right * input.x;
        _blackboard.movement = horizontal + vertical;
        _blackboard.movement = Vector3.ProjectOnPlane(_blackboard.movement, _blackboard.frontWallHit.normal);
    }

    public void SetAnimancer()
    {
        if (_blackboard.inputSO.move == Vector2.zero)
        {
            _normalBodyLayer.Play(_idleClimbAnim);
        }
        else
        {
            _climbBlendTree.State.Parameter = Mathf.Lerp(_climbBlendTree.State.Parameter, Vector3.Angle(_blackboard.playerController.transform.right, _blackboard.movement.normalized), 60 * Time.deltaTime);
        }
    }
}

