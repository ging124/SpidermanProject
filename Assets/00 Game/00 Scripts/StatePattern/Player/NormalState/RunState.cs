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
        _normalBodyLayer.Play(_runBlendTree);
        _blackboard.character.Sprint();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        Movement();
        _runBlendTree.State.Parameter = Mathf.Lerp(_runBlendTree.State.Parameter, _blackboard.character.GetSpeed(), 40 * Time.deltaTime);

        if (_blackboard.inputSO.buttonJump && _blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateManager.stateReferences.startJumpHighState);
            return;
        }

        if (!_blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
            return;
        }

        if (_blackboard.inputSO.move == Vector2.zero)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.stopRunState);
            return;
        }
    }

    public override void ExitState()
    {
        _blackboard.character.StopSprinting();
        base.ExitState();
    }
}
