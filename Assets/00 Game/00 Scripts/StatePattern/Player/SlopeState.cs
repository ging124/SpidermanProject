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
        Slope();

        if(_blackboard.onGround && !_blackboard.onSlope)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.endJumpState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    void Slope()
    {
        if (_blackboard.onSlope)
        {
            _blackboard.charController.Move(-_blackboard.slopeVelocity.normalized * 6 * Time.deltaTime);
        }
    }
}
