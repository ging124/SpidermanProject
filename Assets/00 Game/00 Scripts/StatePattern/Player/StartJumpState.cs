using Animancer;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartJumpState : AirborneMoveState
{
    [SerializeField] private ClipTransition _startJumpAnim;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_startJumpAnim);
        Jump();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    void Jump()
    {
        if(_blackboard.character.IsGrounded())
        {
            _blackboard.character.Jump();
        }
    }
}
