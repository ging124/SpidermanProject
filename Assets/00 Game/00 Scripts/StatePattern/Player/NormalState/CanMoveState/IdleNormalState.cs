using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

public class IdleNormalState : MovementState
{
    [SerializeField] private ClipTransition _idleNormalAnim;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_idleNormalAnim);
        _blackboard.playerController.transform.DORotate(Quaternion.LookRotation(_blackboard.playerController.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles, 0.2f);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        if (_blackboard.inputSO.buttonJump && _blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateReferences.jumpState);
            return;
        }

        if (_blackboard.inputSO.move != Vector2.zero)
        {
            _stateManager.ChangeState(_stateReferences.moveState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
