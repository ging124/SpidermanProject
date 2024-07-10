using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeState : NormalState
{
    [SerializeField] private ClipTransition _slopeAnim;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_slopeAnim);
    }

    public override void UpdateState()
    {
        if(_blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
