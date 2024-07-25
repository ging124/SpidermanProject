using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRunState : MovementState
{
    [SerializeField] private ClipTransition _stopRunAnim;
    [SerializeField] private float _timeChangeState = 0.5f;

    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_stopRunAnim);
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
            _stateManager.ChangeState(_stateManager.stateReferences.jumpState);
            return;
        }

        if (_blackboard.inputSO.move != Vector2.zero)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.moveState);
            return;
        }

        if (_blackboard.inputSO.move == Vector2.zero && _elapsedTime > _timeChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleNormalState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
