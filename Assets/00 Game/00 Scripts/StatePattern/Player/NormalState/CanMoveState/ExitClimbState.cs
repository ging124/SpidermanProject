using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitClimbState : NormalState
{
    [SerializeField] private ClipTransition _exitClimbAnim;
    [SerializeField] private float _timeToChangeState = 0.25f;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.character.useRootMotion = true;
        _blackboard.character.SetRotationMode(RotationMode.OrientWithRootMotion);
        _normalBodyLayer.Play(_exitClimbAnim);
    }
        
    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.playerController.wallFront)
        {
            _stateManager.ChangeState(_stateReferences.climbState);
            return StateStatus.Success;
        }
            
        if (!_blackboard.playerController.wallFront && _blackboard.character.IsGrounded() && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateReferences.fallState);
            return StateStatus.Success;
        }

        if (!_blackboard.playerController.wallFront && _blackboard.inputSO.move != Vector2.zero && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateReferences.runState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.character.useRootMotion = false;
        _blackboard.character.SetRotationMode(RotationMode.OrientToMovement);
        base.ExitState();
    }

}
