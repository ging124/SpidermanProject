using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class ClimbState : CanMoveState
{
    [SerializeField] private ClipTransition _idleClimbAnim;
    [SerializeField] private float _timeToIdle;
    [SerializeField] private float _climbForce;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.playerController.transform.DORotate(Quaternion.LookRotation(_blackboard.playerController.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles, 0.2f);
        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.isKinematic = false;
        _blackboard.playerController.rb.useGravity = false;
        _blackboard.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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

        if(_blackboard.inputSO.buttonJump)
        {
            _blackboard.rb.AddForce(-_blackboard.playerController.transform.forward * _climbForce);
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
        _blackboard.character.SetRotationMode(RotationMode.OrientToMovement);
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.rb.constraints = RigidbodyConstraints.None;
        base.ExitState();
    }
}
