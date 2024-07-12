using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNormalState : MovementState
{
    [SerializeField] private ClipTransition _idleNormalAnim;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_idleNormalAnim);
        
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_stateManager.currentState != this)
        {
            return;
        }

        if (_blackboard.inputSO.buttonJump && _blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateManager.stateReferences.startJumpState);
            return;
        }

        if (_blackboard.inputSO.move != Vector2.zero)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.moveState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }   
}
