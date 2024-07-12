using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementState
{
    [SerializeField] private LinearMixerTransition _runBlendTree;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.character.Sprint();
        _normalBodyLayer.Play(_runBlendTree);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        Movement();
        _runBlendTree.State.Parameter = Mathf.Lerp(_runBlendTree.State.Parameter, _blackboard.character.GetSpeed(), 55 * Time.deltaTime);

        if (_blackboard.inputSO.buttonJump && _blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateManager.stateReferences.startJumpHighState);
            return;
        }

        if (!_blackboard.inputSO.buttonRun)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.stopRunState);
            return;
        }

        if (!_blackboard.inputSO.buttonRun && _blackboard.inputSO.move.magnitude != 0)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.moveState);
            return;
        }
    }

    public override void ExitState()
    {
        _blackboard.character.StopSprinting();
        base.ExitState();
    }
}
