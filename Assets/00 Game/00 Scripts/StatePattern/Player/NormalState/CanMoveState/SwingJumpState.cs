using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

public class SwingJumpState : OnAirState
{
    [SerializeField] private ClipTransition _swingJumpAnim;
    [SerializeField] private float _swingJumpForce;
    [SerializeField] private float _timeToOnAir = 0.5f;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_swingJumpAnim, 0.25f , FadeMode.FromStart);
        _blackboard.character.AddForce((_blackboard.character.GetVelocity() * 2 + Vector3.up * 70) * _swingJumpForce);
        _blackboard.playerController.detectionLength = 3;

    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_elapsedTime > _timeToOnAir)
        {
            _stateManager.ChangeState(_stateReferences.fallState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.playerController.detectionLength = 1;
        _blackboard.playerController.rb.DORotate(Quaternion.LookRotation(_blackboard.playerController.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles, 0.2f);
        base.ExitState();
    }
}
