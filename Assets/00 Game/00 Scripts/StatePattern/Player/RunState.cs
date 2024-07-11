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

        if (!_blackboard.inputSO.buttonRun && _blackboard.inputSO.move != Vector2.zero)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.moveState);
            return;
        }

        if (_blackboard.inputSO.move == Vector2.zero)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.stopMoveState);
            return;
        }
    }

    public override void ExitState()
    {
        _blackboard.character.StopSprinting();
        base.ExitState();
    }
}
