using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

public class SwingJumpState : AirborneMoveState
{
    [SerializeField] private ClipTransition _swingJumpAnim;
    [SerializeField] private float _swingJumpForce;
    [SerializeField] private float _timeToChangeState = 0.5f;
    [SerializeField] private float _timeToOnAir = 0.5f;


    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_swingJumpAnim);
        _blackboard.character.AddForce((_blackboard.character.GetVelocity() + Vector3.up * 10) * _swingJumpForce);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.character.IsGrounded() && _blackboard.character.GetVelocity().magnitude == 0 && _blackboard.inputSO.move == Vector2.zero && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.character.GetVelocity().magnitude < 5 && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpToWalkState);
            return;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.character.GetVelocity().magnitude >= 5 && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpToRunState);
            return;
        }

        if (_elapsedTime > _timeToOnAir)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
            return;
        }
    }

    public override void ExitState()
    {
        _blackboard.playerController.transform.DORotate(Quaternion.LookRotation(_blackboard.playerController.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles, 0.2f);
        base.ExitState();
    }
}
