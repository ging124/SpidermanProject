using Animancer;
using NodeCanvas.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandLowState : GroundState
{
    [SerializeField] private ClipTransition _landLowAnim;
    [SerializeField] private float _timeToChangeState = 0.5f;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_landLowAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.playerController.movement.magnitude > 0.1f) _blackboard.character.SetMovementDirection(_blackboard.playerController.movement);
        else _blackboard.character.SetMovementDirection(_blackboard.playerController.transform.forward);

        if (_blackboard.character.IsGrounded() && _blackboard.playerController.movement == Vector3.zero  && _elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateReferences.idleNormalState);
            return StateStatus.Success;
        }

        if (_blackboard.character.IsGrounded() && _blackboard.playerController.movement != Vector3.zero && _elapsedTime > _timeToChangeState)
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
