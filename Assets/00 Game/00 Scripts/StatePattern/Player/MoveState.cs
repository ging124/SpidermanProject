using Animancer;
using UnityEngine;

public class MoveState : MovementState
{
    [SerializeField] private ClipTransition _moveAnim;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_moveAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        Movement(_blackboard.playerData.moveSpeed);

        if (_blackboard.inputSO.move == Vector2.zero)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.stopMoveState);
            return;
        }

        if (_blackboard.inputSO.buttonRun)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.runState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
