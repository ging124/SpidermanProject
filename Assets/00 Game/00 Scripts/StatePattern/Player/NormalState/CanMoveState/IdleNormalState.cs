using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

public class IdleNormalState : GroundState
{
    [SerializeField] private ClipTransition _idleNormalAnim;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_idleNormalAnim);
        _blackboard.character.SetMovementDirection(Vector3.zero);
        _blackboard.playerController.transform.DORotate(Quaternion.LookRotation(_blackboard.playerController.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles, 0.2f);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_stateManager.currentState != this)
        {
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.move != Vector2.zero && _stateManager.currentState)
            //&& _stateManager.currentState != _stateReferences.useGadgetState)
        {
            _stateManager.ChangeState(_stateReferences.runState);
            return StateStatus.Success;
        }



        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
