using Animancer;
using UnityEngine;

public class IdleZipState : NormalState
{
    [SerializeField] private ClipTransition _idleZipAnim;

    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_idleZipAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        if (_blackboard.inputSO.move != Vector2.zero)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.runState);
            return;
        }

        if (_blackboard.inputSO.buttonJump)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.zipJumpState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
