using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : GroundState
{
    [SerializeField] private LinearMixerTransition _runBlendTree;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_runBlendTree);
        _blackboard.character.Sprint();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        Movement();
        _runBlendTree.State.Parameter = Mathf.Lerp(_runBlendTree.State.Parameter, _blackboard.character.GetSpeed(), 40 * Time.deltaTime);

        if (((StateManagerMovement)_stateManager).stateManagerAction.currentState == _stateReferences.useGadgetState
            || ((StateManagerMovement)_stateManager).stateManagerAction.currentState == _stateReferences.ultimateAttackState)
        {
            _stateManager.ChangeState(_stateReferences.idleNormalState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.move == Vector2.zero)
        {
            _stateManager.ChangeState(_stateReferences.stopRunState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.character.StopSprinting();
        base.ExitState();
    }

    protected virtual void Movement()
    {
        GetInput();

        _blackboard.character.SetMovementDirection(_blackboard.playerController.movement.normalized);
    }

    protected virtual void GetInput()
    {
        Vector2 input = _blackboard.inputSO.move;
        Vector3 vertical = _blackboard.playerController.cam.transform.forward * input.y;
        Vector3 horizontal = _blackboard.playerController.cam.transform.right * input.x;
        _blackboard.playerController.movement = (vertical + horizontal);
        _blackboard.playerController.movement.y = 0;
    }
}
