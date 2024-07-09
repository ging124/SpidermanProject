using Animancer;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartJumpState : AirborneMoveState
{
    [SerializeField] private ClipTransition _startJumpAnim;
    [SerializeField] private float _timeToJump = 0.2f;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_startJumpAnim);
        Invoke(nameof(Jump), _timeToJump);
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
        if(_blackboard.onGround)
        {
            _blackboard.velocity.y = Mathf.Sqrt(_blackboard.playerData.jumpHeight * 9.81f);
        }
    }
}
