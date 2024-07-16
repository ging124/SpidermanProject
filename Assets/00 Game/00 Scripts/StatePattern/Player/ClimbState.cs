using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class ClimbState : CanMoveState
{
    [SerializeField] private LinearMixerTransition _climbBlendTree;
    [SerializeField] private float _timeToIdle;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.playerController.transform.DORotate(Quaternion.LookRotation(_blackboard.playerController.transform.up, -_blackboard.playerController.transform.forward).eulerAngles, 0);
        _normalBodyLayer.Play(_climbBlendTree);
        _blackboard.character.SetRotationMode(RotationMode.None);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        if (!_blackboard.character.IsGrounded() && _elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleNormalState);
            return;
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        Movement();
        _climbBlendTree.State.Parameter = Mathf.Lerp(_climbBlendTree.State.Parameter, _blackboard.character.GetSpeed(), 55 * Time.deltaTime);

    }

    public override void ExitState()
    {
        _blackboard.character.SetRotationMode(RotationMode.OrientToMovement);
        base.ExitState();
    }

    protected override void Movement()
    {
        GetInput();

        _blackboard.character.SetMovementDirection(_blackboard.movement);
    }

    protected override void GetInput()
    {
        Vector2 input = _blackboard.inputSO.move;
        Vector3 vertical = _blackboard.playerController.transform.forward * input.y;
        Vector3 horizontal = _blackboard.playerController.transform.right * input.x;
        RaycastHit hit;
        _blackboard.movement = horizontal + vertical;
        Physics.Raycast(_blackboard.playerController.transform.position + _blackboard.playerController.transform.up, -_blackboard.playerController.transform.up, out hit);
        _blackboard.movement = Vector3.ProjectOnPlane(_blackboard.movement, hit.normal);
    }

}
